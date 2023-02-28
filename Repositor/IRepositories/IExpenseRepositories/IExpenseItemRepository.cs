using Model.DataTableModels;
using Model.EntityModels.ExpenseModels;
using Repository.IBaseRepository;
using System.Threading.Tasks;
using Model.DtoModels.ExpenseDtoModels;

namespace Repository.IRepositories.IExpenseRepositories
{
    public interface IExpenseItemRepository : IBaseRepository<ExpenseItem>
    {
        Task<DataTablePagination<ExpenseItemSearchDto, ExpenseItemSearchDto>> Search(DataTablePagination<ExpenseItemSearchDto, ExpenseItemSearchDto> searchDto);
    }
}
