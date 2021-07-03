using PortfolioAPI.Data.Models;
using PortfolioAPI.Models.Enums;
using System.Threading.Tasks;

namespace PortfolioAPI.Data.Repositories
{
    public interface IMailRepository
    {
        #region Settings
        Task AddSettingAsync(MailSetting setting);
        Task UpdateSettingAsync(MailSetting setting);
        Task<MailSetting> GetSettingAsync(MailProviderCode providerCode);
        #endregion
    }
}
