using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data.Models;
using PortfolioAPI.Models.Enums;
using System.Threading.Tasks;

namespace PortfolioAPI.Data.Repositories
{
    public class MailRepository : IMailRepository
    {
        private readonly IContext _context;

        public MailRepository(IContext context) => _context = context;

        public async Task AddSettingAsync(MailSetting setting)
        {
            await _context.MailSettings.AddAsync(setting);
            await _context.SaveChangesAsync();
        }

        public async Task<MailSetting> GetSettingAsync(MailProviderCode providerCode)
        {
            return await _context.MailSettings.FirstOrDefaultAsync(x => x.ProviderCode == providerCode);
        }
    }
}
