using Lab_Test.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.IRepositories;
using Repository.Repositories;
using Resolver.ModelMapper;
using Service.IServices;
using Service.Services;

namespace Resolver.DependencyResolver
{
    public class DependencyResolverProfile
    {
        public void ConfigServiceMap(IServiceCollection services)
        {
            services.AddTransient<DbContext, LabTestDbContext>();

            services.AddTransient<IExpenseItemRepository, ExpenseItemRepository>();
            services.AddTransient<IExpenseItemService, ExpenseItemService>();


            #region AutoMapper

            services.AddAutoMapper(typeof(ModelMapperProfile));

            #endregion
        }
    }
}
