using AutoMapper;
using Model.DtoModels.ExpenseDtoModels;
using Model.EntityModels.ExpenseModels;
using Repository.IRepositories.IExpenseRepositories;
using Service.BaseService;
using Service.IServices.IExpenseServices;
using System.Threading.Tasks;
using System;
using Model.DataTablePaginationModels;

namespace Service.Services.ExpenseServices
{
    public class ExpenseService : BaseService<Expense>, IExpenseService
    {
        private IExpenseRepository Repository { get; set; }
        private readonly IMapper _iMapper;
        public ExpenseService(IExpenseRepository iRepository, IMapper iMapper) : base(iRepository)
        {
            Repository = iRepository;
            _iMapper = iMapper;
        }

        public async Task<bool> AddAsync(ExpenseDto dto)
        {
            if (dto == null) return false;
            var model = _iMapper.Map<Expense>(dto);
            var result = await Repository.AddAsync(model);
            dto.Id = model.Id;
            return result;
        }

        public async Task<bool> UpdateAsync(ExpenseDto dto)
        {
            if (dto == null) return false;
            var model = _iMapper.Map<Expense>(dto);
            var result = await Repository.UpdateAsync(model);
            return result;
        }

        public new async Task<ExpenseDto> GetByIdAsync(int id)
        {
            if (id == 0) return null;
            var data = await Repository.GetByIdAsync(id);
            var dto = _iMapper.Map<ExpenseDto>(data);
            return dto;
        }

        public ExpenseDto ConvertModelToDto(Expense model)
        {
            if (model == null) return null;
            var dto = _iMapper.Map<ExpenseDto>(model);
            return dto;
        }

        public async Task<DataTablePagination<ExpenseSearchDto>> Search(DataTablePagination<ExpenseSearchDto> searchDto)
        {
            searchDto ??= new DataTablePagination<ExpenseSearchDto>();
            var dataTable = await Repository.Search(searchDto);
            return dataTable;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id == 0) return false;
            var data = await Repository.GetByIdAsync(id);
            if (data == null) throw new Exception($"Sorry, No data found!");
            var result = await Repository.RemoveAsync(data, true);
            return result;
        }
    }
}
