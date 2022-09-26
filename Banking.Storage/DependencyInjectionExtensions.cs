using Banking.Domain.Contracts;
using Banking.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Storage
{
    public static class DependencyInjectionExtensions
    {
        public static void AddStorage(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAccountQueries, AccountRepository>();
            //services.AddTransient<IAccountRepository, AccountRepository>();
            //On purpose this line is not added. Should be resolved from UnitOfWork.
            services.AddDbContext<AccountContext>(options =>
                options.UseInMemoryDatabase("AccountDb"),
                ServiceLifetime.Transient);
        }
    }
}
