using AutoMapper;
using Model.DataTablePaginationModels;
using Model.DtoModels.ExpenseDtoModels;
using Model.EntityModels.ExpenseModels;
using Repository.IRepositories.IExpenseRepositories;
using Service.BaseService;
using Service.IServices.IExpenseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services.ExpenseServices
{
    public class ExpenseService : BaseService<Expense>, IExpenseService
    {
        private IExpenseRepository Repository { get; set; }
        private readonly IExpenseDetailService _iExpenseDetailService;
        private readonly IMapper _iMapper;
        public ExpenseService(IExpenseRepository iRepository, IMapper iMapper, IExpenseDetailService iExpenseDetailService) : base(iRepository)
        {
            Repository = iRepository;
            _iMapper = iMapper;
            _iExpenseDetailService = iExpenseDetailService;
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

            if (dto.Details is not { Count: > 0 }) throw new Exception("Sorry! No Expense Item Found to Create/ Update!");

            dto.Details = dto.GetCheckedItemSameOrNot();

            var model = _iMapper.Map<Expense>(dto);
            if (model == null) throw new Exception("Sorry! No Data Found!");
            // model.Details = _iMapper.Map<List<ExpenseDetail>>(dto.Details);

            if (model.Id <= 0) return false;

            var addableList = model.Details?.Where(c => c.Id == 0).ToList();
            var updateableList = model.Details?.Where(c => c.Id > 0).ToList();
            var oldIds = updateableList?.Select(c => c.Id).ToList();
            var deletableList = (model.Id > 0 && oldIds?.Count > 0) ? (await _iExpenseDetailService.GetAsync(c => c.ExpenseId == dto.Id && !oldIds.Contains(c.Id))).ToList() : new List<ExpenseDetail>();
            if (updateableList is { Count: 0 } && (deletableList.Count == 0)) deletableList = (await _iExpenseDetailService.GetAsync(c => c.ExpenseId == dto.Id)).ToList();

            if (addableList?.Count > 0)
            {
                addableList.ForEach(c => c.ExpenseId = model.Id);
                var isDetailAdded = await _iExpenseDetailService.AddRangeAsync(addableList);
                if (!isDetailAdded) return false;
            }

            if (updateableList?.Count > 0)
            {
                var isDetailUpdated = await _iExpenseDetailService.UpdateRangeAsync(updateableList);
                if (!isDetailUpdated) return false;
            }

            if (deletableList.Count > 0)
            {
                var isDetailDeleted = await _iExpenseDetailService.RemoveRangeAsync(deletableList, true);
                if (!isDetailDeleted) return false;
            }

            model.Details = null;
            var isUpdated = await Repository.UpdateAsync(model);
            if (!isUpdated) return false;

            dto.Id = model.Id;
            return true;
        }

        public new async Task<ExpenseDto> GetByIdAsync(int id)
        {
            if (id == 0) return null;
            var model = await Repository.GetByIdAsync(id);
            var dto = ConvertModelToDto(model);
            return dto;
        }

        public ExpenseDto ConvertModelToDto(Expense model)
        {
            if (model == null) return null;
            var dto = _iMapper.Map<ExpenseDto>(model);
            if (model.Details is { Count: > 0 })
            {
                dto.Details = _iExpenseDetailService.ConvertModelToDto(model.Details.ToList());
            }
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
