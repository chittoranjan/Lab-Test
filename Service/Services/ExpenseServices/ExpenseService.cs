using Model.EntityModels.ExpenseModels;
using Repository.IRepositories.IExpenseRepositories;
using Service.BaseService;
using Service.IServices.IExpenseServices;

namespace Service.Services.ExpenseServices
{
    public class ExpenseService : BaseService<Expense>, IExpenseService
    {
        private IExpenseRepository Repository { get; set; }
        public ExpenseService(IExpenseRepository iRepository) : base(iRepository)
        {
            Repository = iRepository;
        }
    }
}
