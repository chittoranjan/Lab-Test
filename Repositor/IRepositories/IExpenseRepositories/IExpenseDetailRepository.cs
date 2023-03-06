using Model.DataTablePaginationModels;
using Model.DtoModels.ExpenseDtoModels;
using Model.EntityModels.ExpenseModels;
using Repository.IBaseRepository;
using System.Threading.Tasks;

namespace Repository.IRepositories.IExpenseRepositories
{
    public interface IExpenseDetailRepository : IBaseRepository<ExpenseDetail>
    {
        Task<DataTablePagination<ExpenseDetailSearchDto>> Search(DataTablePagination<ExpenseDetailSearchDto> searchDto);
    }
}
