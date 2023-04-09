using AutoMapper;
using Model.DataTablePaginationModels;
using Model.DtoModels.ExpenseDtoModels;
using Model.EntityModels.ExpenseModels;
using Repository.IRepositories.IExpenseRepositories;
using Service.BaseService;
using Service.DistributedRedisCache;
using Service.IServices.IExpenseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services.ExpenseServices
{
    public class ExpenseItemService : BaseService<ExpenseItem>, IExpenseItemService
    {
        private IExpenseItemRepository Repository { get; set; }
        private readonly IMapper _iMapper;
        private readonly ICacheService _iCacheService;
        public ExpenseItemService(IExpenseItemRepository iRepository, IMapper iMapper, ICacheService iCacheService) : base(iRepository)
        {
            Repository = iRepository;
            _iMapper = iMapper;
            _iCacheService = iCacheService;
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
            if (id == 0) return null;
            var model = await Repository.GetByIdAsync(id);
            var dto = ConvertModelToDto(model);
            return dto;
        }

        public ExpenseItemDto ConvertModelToDto(ExpenseItem model)
        {
            if (model == null) return null;
            var dto = _iMapper.Map<ExpenseItemDto>(model);
            return dto;
        }

        public async Task<DataTablePagination<ExpenseItemSearchDto>> Search(DataTablePagination<ExpenseItemSearchDto> searchDto)
        {
            searchDto ??= new DataTablePagination<ExpenseItemSearchDto>();
            var dataTable = await Repository.Search(searchDto);
            return dataTable;
        }

        public async Task<List<ExpenseItem>> GetSelectionListAsync()
        {
            var expItemCacheData = await _iCacheService.GetStringAsync<ExpenseItem>(CacheKeyName.ExpenseItem.ToString());
            if (expItemCacheData.Count > 0) return expItemCacheData;

            var result = await Repository.GetAllAsync();
            var data = result.ToList();
            data.Insert(0, new ExpenseItem() { Id = 0, Name = "Select Item" });
            expItemCacheData = data.ToList<ExpenseItem>();
            var isCached = await _iCacheService.SetStringAsync(CacheKeyName.ExpenseItem.ToString(), expItemCacheData);
            return expItemCacheData;
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
