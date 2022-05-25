using Model.EntityModels.ExpenseModels;
using Repository.IRepositories;
using Service.BaseService;
using Service.IServices;

namespace Service.Services
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
