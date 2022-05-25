using Model.EntityModels.ExpenseModels;
using Repository.IBaseRepository;

namespace Repository.IRepositories
{
    public interface IExpenseItemRepository : IBaseRepository<ExpenseItem>
    {
    }
}
