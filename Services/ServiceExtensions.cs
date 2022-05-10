using Microsoft.Extensions.DependencyInjection;

namespace NorthwindAPI.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<ICategoryPluralBuilder, CategoryPluralBuilder>();
            services.AddScoped<ICustomerCustomerDemoBuilder, CustomerCustomerDemoBuilder>();
            services.AddScoped<ICustomerDemographicPluralBuilder, CustomerDemographicPluralBuilder>();
            services.AddScoped<ICustomerPluralBuilder, CustomerPluralBuilder>();
            services.AddScoped<IEmployeePluralBuilder, EmployeePluralBuilder>();
            services.AddScoped<IEmployeeTerritoryPluralBuilder, EmployeeTerritoryPluralBuilder>();
            services.AddScoped<IOrderDetailPluralBuilder, OrderDetailPluralBuilder>();
            services.AddScoped<IOrderPluralBuilder, OrderPluralBuilder>();
            services.AddScoped<IProductPluralBuilder, ProductPluralBuilder>();
            services.AddScoped<IRegionBuilder, RegionBuilder>();
            services.AddScoped<IShipperPluralBuilder, ShipperPluralBuilder>();
            services.AddScoped<ISupplierPluralBuilder, SupplierPluralBuilder>();
            services.AddScoped<ITerritoryPluralBuilder, TerritoryPluralBuilder>();
        }
    }
}
