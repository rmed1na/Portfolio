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
    [Authorize]
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
        [Route("send")]
        public async Task<bool> SendMailAsync(SendMailRequest request)
        {
            //TODO: Register a log
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
            if (request.ProviderCode == MailProviderCode.Unknown)
                throw new ArgumentException("Invalid mail provider code");

            if (request.Port == default(int))
                throw new ArgumentException("A port must be defined");

            if (string.IsNullOrWhiteSpace(request.Sender) || string.IsNullOrWhiteSpace(request.HostServer) || string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("A sender, host server and password must be defined");

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

        [HttpPut]
        [Route("provider/update")]
        public async Task UpdateProviderAsync(RegisterProviderRequest request)
        {
            var setting = await _mailRepository.GetSettingAsync(request.ProviderCode);

            if (setting == null)
                throw new ArgumentException($"Mail provider not found");

            setting.Host = request.HostServer ?? setting.Host;
            setting.Port = request.Port;
            setting.Sender = request.Sender;
            setting.Password = EncryptionUtility.Encrypt(request.Password);

            await _mailRepository.UpdateSettingAsync(setting);
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
