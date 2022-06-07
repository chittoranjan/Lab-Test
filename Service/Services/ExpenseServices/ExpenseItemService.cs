using Model.EntityModels.ExpenseModels;
using Repository.IRepositories.IExpenseRepositories;
using Service.BaseService;
using Service.IServices.IExpenseServices;

namespace Service.Services.ExpenseServices
{
    public class ExpenseItemService : BaseService<ExpenseItem>, IExpenseItemService
    {
        private IExpenseItemRepository Repository { get; set; }
        public ExpenseItemService(IExpenseItemRepository iRepository) : base(iRepository)
        {
            Repository = iRepository;
        }
    }
}
