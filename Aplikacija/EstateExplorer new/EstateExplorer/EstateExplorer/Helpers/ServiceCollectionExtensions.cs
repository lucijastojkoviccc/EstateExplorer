using EstateExplorer.Helpers.Currency.Models;

namespace EstateExplorer.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddHttpClient<CurrencyHttpClient>();

            return services;
        }
    }
}
