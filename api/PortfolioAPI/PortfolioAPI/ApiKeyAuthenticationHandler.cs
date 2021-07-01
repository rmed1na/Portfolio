﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PortfolioAPI.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace PortfolioAPI.Models
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private const string ProblemDetailsContentType = "application/problem+json";
        private readonly IApiKeyRepository _apiKeyRepository;
        private const string ApiKeyHeaderName = "X-Api-Key";

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IApiKeyRepository apiKeyRepository) : base(options, logger, encoder, clock)
        {
            _apiKeyRepository = apiKeyRepository ?? throw new ArgumentNullException(nameof(apiKeyRepository));
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
                return AuthenticateResult.NoResult();

            var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

            if (apiKeyHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(providedApiKey))
                return AuthenticateResult.NoResult();

            var userApiKey = await _apiKeyRepository.GetUserApiKeyAsync(providedApiKey);

            if (userApiKey != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userApiKey.User.GetFullName()),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
                var identities = new List<ClaimsIdentity> { identity };
                var principal = new ClaimsPrincipal(identities);
                var ticket = new AuthenticationTicket(principal, Options.Scheme);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.NoResult();
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            Response.ContentType = ProblemDetailsContentType;

            await Response.WriteAsync(JsonSerializer.Serialize("Unauthorized access"));
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            Response.ContentType = ProblemDetailsContentType;

            await Response.WriteAsync(JsonSerializer.Serialize("Forbidden access"));
        }
    }
}
