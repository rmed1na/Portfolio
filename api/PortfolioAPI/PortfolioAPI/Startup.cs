using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using PortfolioAPI.Data;
using PortfolioAPI.Data.Repositories;
using PortfolioAPI.Extensions;
using PortfolioAPI.Models;
using System;

namespace PortfolioAPI
{
    public class Startup
    {
        private readonly string _specificOriginPolicy = "AllowSpecificOrigin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = Configuration.GetSection("Api").Get<AppSettings>();
            var dbConnnectionString = BuildMySqlConnectionString(appSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            }).AddApiKeySupport(options => { });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PortfolioAPI", Version = "v1" });
            });
            
            services.AddDbContext<Context>(x => x.UseMySql(dbConnnectionString, ServerVersion.AutoDetect(dbConnnectionString)));
            
            services.AddScoped<IContext, Context>();
            services.AddScoped<IApiKeyRepository, ApiKeyRepository>();
            services.AddScoped<IMailRepository, MailRepository>();

            services.AddSingleton<IAppSettings>(appSettings);
            services.AddCors(options =>
            {
                options.AddPolicy(_specificOriginPolicy, builder =>
                {
                    builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PortfolioAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(_specificOriginPolicy);
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region Private
        private string BuildMySqlConnectionString(AppSettings appSettings)
        {
            var sb = new MySqlConnectionStringBuilder
            {
                Server = appSettings.DataSource.Server,
                Database = appSettings.DataSource.Database,
                Port = Convert.ToUInt32(appSettings.DataSource.Port ?? "3306"),
                UserID = appSettings.DataSource.User,
                Password = appSettings.DataSource.Password
            };

            return sb.ConnectionString;
        }
        #endregion Private
    }
}
