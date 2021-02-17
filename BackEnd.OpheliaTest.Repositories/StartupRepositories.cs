using BackEnd.OpheliaTest.Entities.Interface.Repository;
using BackEnd.OpheliaTest.Utilities;
using BackEnd.OpheliaTest.Repositories.Base;
using BackEnd.OpheliaTest.Repositories.DataBase;
using BackEnd.OpheliaTest.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BackEnd.OpheliaTest.Repositories
{
    public static class StartupRepositories
    {

        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration, bool isDev)
        {
            services.AddDbContext<OPHELIATESTContext>(options =>
            {
                options.UseSqlServer(HelperConnection.GetConnectionSQL(configuration, isDev), sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), null);
                    sqlOptions.CommandTimeout(60);
                });
            });

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
        }
    }
}
