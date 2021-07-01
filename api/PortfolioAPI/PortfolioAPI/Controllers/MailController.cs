using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Controllers.Models;
using PortfolioAPI.Data.Models;
using PortfolioAPI.Data.Repositories;
using PortfolioAPI.Models.Enums;
using PortfolioAPI.Utilities;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/v1/mail")]
    public class MailController : ControllerBase
    {
        private const MailProviderCode DEFAULT_MAIL_PROVIDER_CODE = MailProviderCode.Outlook;
        private readonly IMailRepository _mailRepository;

        public MailController(IMailRepository mailRepository)
        {
            _mailRepository = mailRepository;
        }

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="request">Request object</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("send")]
        public async Task<bool> SendMailAsync(SendMailRequest request)
        {
            ValidateMailRequest(request);

            var mailSettings = await _mailRepository.GetSettingAsync(DEFAULT_MAIL_PROVIDER_CODE);
            var sender = new SmtpSender(() => new SmtpClient(mailSettings.Host)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = mailSettings.Port,
                Credentials = new NetworkCredential(mailSettings.Sender, EncryptionUtility.Decrypt(mailSettings.Password))
            });

            Email.DefaultSender = sender;

            request.Message += "\n\nSender information:";
            request.Message += $"\nEmail: {request.From}";
            request.Message += $"\nName: {request.SenderName}";

            var email = await Email
                .From(mailSettings.Sender)
                .To(request.To)
                .Subject(request.Subject)
                .Body(request.Message)
                .SendAsync();

            return email.Successful;
        }

        private void ValidateMailRequest(SendMailRequest request)
        {
            if (string.IsNullOrEmpty(request.Message))
                throw new ArgumentException($"A message must be specified");

            if (string.IsNullOrEmpty(request.To))
                throw new ArgumentException($"A receiver address must be specified");

            if (string.IsNullOrEmpty(request.Subject))
                throw new ArgumentException($"A subject must be specified");
        }

        [HttpPost]
        [Route("provider/register")]
        public async Task RegisterProviderAsync(RegisterProviderRequest request)
        {
            //TODO: Validate request is OK
            await _mailRepository.AddSettingAsync(new MailSetting
            {
                CreatedDate = DateTime.UtcNow,
                StatusCode = GlobalStatusCode.Active,
                ProviderCode = request.ProviderCode,
                Host = request.HostServer,
                Port = request.Port,
                Sender = request.Sender,
                Password = EncryptionUtility.Encrypt(request.Password)
            });
        }

        [HttpGet]
        [Route("provider/{code:int}")]
        public async Task<MailProviderSettingsResponse> GetProviderSettings(MailProviderCode code)
        {
            var setting = await _mailRepository.GetSettingAsync(code);

            return new MailProviderSettingsResponse
            {
                ProviderCode = setting.ProviderCode,
                Host = setting.Host,
                Port = setting.Port,
                Sender = setting.Sender,
                Password = EncryptionUtility.Decrypt(setting.Password)
            };
        }
    }
}
