using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data.Models;
using PortfolioAPI.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PortfolioAPI.Data
{
    public interface IContext
    {
        DbSet<User> Users { get; set; }
        DbSet<ApiKey> ApiKeys { get; set; }
        DbSet<UserApiKey> UserApiKeys { get; set; }
        DbSet<MailSetting> MailSettings { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
