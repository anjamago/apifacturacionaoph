using BackEnd.OpheliaTest.BusinessRules;
using BackEnd.OpheliaTest.Entities.Interface.BusinessRules;
using Microsoft.Extensions.DependencyInjection;

namespace BackEnd.OpheliaTest.BusinessRules
{
    public static class StartupBusiness
    {

        public static void AddBusiness(this IServiceCollection services)
        {
            services.AddTransient<IClienteBusiness, ClienteBusiness>();
            services.AddTransient<ISellerBusiness, SellerBusiness>();

        }
    }
}
