using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.Data.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PortfolioAPI.Models
{
    public class Context : DbContext, IContext
    {
        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<UserApiKey> UserApiKeys { get; set; }
        public DbSet<MailSetting> MailSettings { get; set; }
        #endregion DbSets

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public Context(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            CreateModels(modelBuilder);
        }

        private void CreateModels(ModelBuilder mb)
        {
            mb.ApplyConfiguration(new UserConfiguration());
            mb.ApplyConfiguration(new ApiKeyConfiguration());
            mb.ApplyConfiguration(new UserApiKeyConfiguration());
            mb.ApplyConfiguration(new MailSettingConfiguration());
        }
    }
}
