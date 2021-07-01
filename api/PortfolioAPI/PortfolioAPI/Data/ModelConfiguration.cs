using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioAPI.Data.Models;
using PortfolioAPI.Models;

namespace PortfolioAPI.Data
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.UserId);
        }
    }

    internal class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
    {
        public void Configure(EntityTypeBuilder<ApiKey> builder)
        {
            builder.ToTable("ApiKeys");
            builder.HasKey(x => x.ApiKeyId);
        }
    }

    internal class UserApiKeyConfiguration : IEntityTypeConfiguration<UserApiKey>
    {
        public void Configure(EntityTypeBuilder<UserApiKey> builder)
        {
            builder.ToTable("UserApiKeys");
            builder.HasKey(x => x.UserApiKeyId);
        }
    }

    internal class MailSettingConfiguration : IEntityTypeConfiguration<MailSetting>
    {
        public void Configure(EntityTypeBuilder<MailSetting> builder)
        {
            builder.ToTable("MailSettings");
            builder.HasKey(x => x.MailSettingId);
        }
    }
}
