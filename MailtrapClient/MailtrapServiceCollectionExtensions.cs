using MailtrapClient.Services;
using MailtrapClient.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Configuration;
using System.Net.Http.Headers;

namespace MailtrapClient
{
    public static class MailtrapServiceCollectionExtensions
    {
        private static string MailtrapApiSettingsName = "MailtrapApi";

        public static IServiceCollection AddMailtrapService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<MailtrapApiSettings>()
                .Configure<IConfiguration>((opt, conf) => { conf.GetSection(MailtrapApiSettingsName).Bind(opt); });

            var mailtrapApiConfig = configuration.GetSection(MailtrapApiSettingsName).Get<MailtrapApiSettings>();

            if (mailtrapApiConfig is null)
                throw new SettingsPropertyNotFoundException($"'{MailtrapApiSettingsName}' settings was not found.");

            if (mailtrapApiConfig.BaseAddress is null)
                throw new SettingsPropertyNotFoundException($"'{MailtrapApiSettingsName}.{nameof(mailtrapApiConfig.BaseAddress)}' settings was not found.");

            if (string.IsNullOrEmpty(mailtrapApiConfig.ApiToken))
                throw new SettingsPropertyNotFoundException($"'{MailtrapApiSettingsName}.{nameof(mailtrapApiConfig.BaseAddress)}' settings was not found.");

            services.AddRefitClient<IMailtrapClient>()
                .ConfigureHttpClient(_ =>
                {
                    _.BaseAddress = mailtrapApiConfig.BaseAddress;
                    _.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mailtrapApiConfig.ApiToken);
                });

            services.AddScoped<IMailtrapEmailService, MailtrapEmailService>();

            return services;
        }
    }
}
