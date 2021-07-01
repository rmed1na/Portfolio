using PortfolioAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAPI.Data.Repositories
{
    public interface IApiKeyRepository
    {
        Task<UserApiKey> GetUserApiKeyAsync(string apiKey);
    }
}
