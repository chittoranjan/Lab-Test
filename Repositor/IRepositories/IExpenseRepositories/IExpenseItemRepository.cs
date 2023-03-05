using Model.EntityModels.ExpenseModels;
using Repository.IBaseRepository;
using System.Threading.Tasks;
using Model.DtoModels.ExpenseDtoModels;
using Model.DataTablePaginationModels;

namespace Repository.IRepositories.IExpenseRepositories
{
    public interface IExpenseItemRepository : IBaseRepository<ExpenseItem>
    {
        Task<DataTablePagination<ExpenseItemSearchDto>> Search(DataTablePagination<ExpenseItemSearchDto> searchDto);
    }
}
