using Model.EntityModels.ExpenseModels;
using Repository.IRepositories.IExpenseRepositories;
using Service.BaseService;
using Service.IServices.IExpenseServices;

namespace Service.Services.ExpenseServices
{
    public class ExpenseDetailService : BaseService<ExpenseDetail>, IExpenseDetailService
    {
        private IExpenseDetailRepository Repository { get; set; }
        public ExpenseDetailService(IExpenseDetailRepository iRepository) : base(iRepository)
        {
            Repository = iRepository;
        }
    }
}
