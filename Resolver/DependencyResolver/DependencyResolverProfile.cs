using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectContext.ProjectDbContext;
using Repository.IRepositories.IExpenseRepositories;
using Repository.Repositories.ExpenseRepositories;
using Resolver.ModelMapper;
using Service.IServices.IExpenseServices;
using Service.Services.ExpenseServices;

namespace Resolver.DependencyResolver
{
    public class DependencyResolverProfile
    {
        public void ConfigServiceMap(IServiceCollection services)
        {
            services.AddTransient<DbContext, LabTestDbContext>();

            #region Expense Dependency Resolve
            services.AddTransient<IExpenseItemRepository, ExpenseItemRepository>();
            services.AddTransient<IExpenseItemService, ExpenseItemService>();

            services.AddTransient<IExpenseRepository, ExpenseRepository>();
            services.AddTransient<IExpenseService, ExpenseService>();

            services.AddTransient<IExpenseDetailRepository, ExpenseDetailRepository>();
            services.AddTransient<IExpenseDetailService, ExpenseDetailService>();
            #endregion

            #region AutoMapper

            services.AddAutoMapper(typeof(ModelMapperProfile));

            #endregion
        }
    }
}
