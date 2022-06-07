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

            services.AddTransient<IExpenseItemRepository, ExpenseItemRepository>();
            services.AddTransient<IExpenseItemService, ExpenseItemService>();


            #region AutoMapper

            services.AddAutoMapper(typeof(ModelMapperProfile));

            #endregion
        }
    }
}
