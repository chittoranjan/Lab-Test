using System;
using Model.DtoModels.ExpenseDtoModels;
using Model.EntityModels.ExpenseModels;
using Repository.IRepositories.IExpenseRepositories;
using Service.BaseService;
using Service.IServices.IExpenseServices;
using System.Threading.Tasks;
using AutoMapper;
using Model.DataTableModels;

namespace Service.Services.ExpenseServices
{
    public class ExpenseItemService : BaseService<ExpenseItem>, IExpenseItemService
    {
        private IExpenseItemRepository Repository { get; set; }
        private readonly IMapper _iMapper;
        public ExpenseItemService(IExpenseItemRepository iRepository, IMapper iMapper) : base(iRepository)
        {
            Repository = iRepository;
            _iMapper = iMapper;
        }

        public async Task<bool> AddAsync(ExpenseItemDto dto)
        {
            if (dto == null) return false;
            var model = _iMapper.Map<ExpenseItem>(dto);
            var result = await Repository.AddAsync(model);
            dto.Id = model.Id;
            return result;
        }

        public async Task<bool> UpdateAsync(ExpenseItemDto dto)
        {
            if (dto == null) return false;
            var model = _iMapper.Map<ExpenseItem>(dto);
            var result = await Repository.UpdateAsync(model);
            return result;
        }

        public new async Task<ExpenseItemDto> GetByIdAsync(int id)
        {
            if (id ==0) return null;
            var data = await Repository.GetByIdAsync(id);
            var dto = _iMapper.Map<ExpenseItemDto>(data);
            return dto;
        }

        public ExpenseItemDto ConvertModelToDto(ExpenseItem model)
        {
            if (model == null) return null;
            var dto = _iMapper.Map<ExpenseItemDto>(model);
            return dto;
        }

        public async Task<DataTablePagination<ExpenseItemSearchDto, ExpenseItemSearchDto>> Search(DataTablePagination<ExpenseItemSearchDto, ExpenseItemSearchDto> searchDto)
        {
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
