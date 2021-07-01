using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Models;
using System.Threading.Tasks;

namespace PortfolioAPI.Data.Repositories
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly IContext _context;

        public ApiKeyRepository(IContext context) => _context = context;

        public async Task<UserApiKey> GetUserApiKeyAsync(string apiKey)
        {
            return await _context.UserApiKeys
                .Include(x => x.ApiKey)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.ApiKey.Key == apiKey);
        }
    }
}
