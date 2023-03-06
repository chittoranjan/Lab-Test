using Model.DataTablePaginationModels;
using Model.DtoModels.ExpenseDtoModels;
using Model.EntityModels.ExpenseModels;
using Repository.IBaseRepository;
using System.Threading.Tasks;

namespace Repository.IRepositories.IExpenseRepositories
{
    public interface IExpenseRepository : IBaseRepository<Expense>
    {
        Task<DataTablePagination<ExpenseSearchDto>> Search(DataTablePagination<ExpenseSearchDto> searchDto);
    }
}
