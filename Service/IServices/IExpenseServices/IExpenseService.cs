using Model.DtoModels.ExpenseDtoModels;
using Model.EntityModels.ExpenseModels;
using Service.IBaseService;
using System.Threading.Tasks;

namespace Service.IServices.IExpenseServices
{
    public interface IExpenseService : IBaseService<Expense>
    {
        Task<bool> AddAsync(ExpenseDto dto);
        Task<bool> UpdateAsync(ExpenseDto dto);
        new Task<ExpenseDto> GetByIdAsync(int id);
        ExpenseDto ConvertModelToDto(Expense model);
        Task<bool> DeleteAsync(int id);
    }
}
