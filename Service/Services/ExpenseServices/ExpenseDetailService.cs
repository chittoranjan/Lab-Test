﻿using AutoMapper;
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
    public class ExpenseDetailService : BaseService<ExpenseDetail>, IExpenseDetailService
    {
        private IExpenseDetailRepository Repository { get; set; }
        private readonly IMapper _iMapper;
        public ExpenseDetailService(IExpenseDetailRepository iRepository, IMapper iMapper) : base(iRepository)
        {
            Repository = iRepository;
            _iMapper = iMapper;
        }

        public async Task<bool> AddAsync(ExpenseDetailDto dto)
        {
            if (dto == null) return false;
            var model = _iMapper.Map<ExpenseDetail>(dto);
            var result = await Repository.AddAsync(model);
            dto.Id = model.Id;
            return result;
        }

        public async Task<bool> UpdateAsync(ExpenseDetailDto dto)
        {
            if (dto == null) return false;
            var model = _iMapper.Map<ExpenseDetail>(dto);
            var result = await Repository.UpdateAsync(model);
            return result;
        }

        public new async Task<ExpenseDetailDto> GetByIdAsync(int id)
        {
            if (id == 0) return null;
            var data = await Repository.GetByIdAsync(id);
            var dto = _iMapper.Map<ExpenseDetailDto>(data);
            return dto;
        }

        public ExpenseDetailDto ConvertModelToDto(ExpenseDetail model)
        {
            if (model == null) return null;
            var dto = _iMapper.Map<ExpenseDetailDto>(model);
            dto.ExpenseItemName = model.ExpenseItem?.Name;
            return dto;
        }

        public List<ExpenseDetailDto> ConvertModelToDto(List<ExpenseDetail> models)
        {
            var dataList = new List<ExpenseDetailDto>();
            if (models is { Count: > 0 })
            {
                dataList.AddRange(models.Select(ConvertModelToDto));
            }

            return dataList;
        }

        public async Task<DataTablePagination<ExpenseDetailSearchDto>> Search(DataTablePagination<ExpenseDetailSearchDto> searchDto)
        {
            searchDto ??= new DataTablePagination<ExpenseDetailSearchDto>();
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
