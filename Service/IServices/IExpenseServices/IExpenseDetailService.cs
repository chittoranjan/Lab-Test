using Model.DtoModels.ExpenseDtoModels;
using Model.EntityModels.ExpenseModels;
using Service.IBaseService;
using System.Threading.Tasks;

namespace Service.IServices.IExpenseServices
{
    public interface IExpenseDetailService : IBaseService<ExpenseDetail>
    {
        Task<bool> AddAsync(ExpenseDetailDto dto);
        Task<bool> UpdateAsync(ExpenseDetailDto dto);
        new Task<ExpenseDetailDto> GetByIdAsync(int id);
        ExpenseDetailDto ConvertModelToDto(ExpenseDetail model);
        Task<bool> DeleteAsync(int id);
    }
}
