using Model.EntityModels.ExpenseModels;
using Service.IBaseService;

namespace Service.IServices.IExpenseServices
{
    public interface IExpenseService : IBaseService<Expense>
    {
    }
}
