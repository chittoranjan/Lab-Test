using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.DataTablePaginationModels;
using Model.DtoModels.ExpenseDtoModels;
using Model.EntityModels.ExpenseModels;
using Model.Utilities;
using ProjectContext.ProjectDbContext;
using Repository.BaseRepository;
using Repository.IRepositories.IExpenseRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories.ExpenseRepositories
{
    public class ExpenseItemRepository : BaseRepository<ExpenseItem>, IExpenseItemRepository
    {
        private LabTestDbContext Context => Db as LabTestDbContext;
        private readonly IMapper _iMapper;
        public ExpenseItemRepository(LabTestDbContext db, IMapper iMapper) : base(db)
        {
            _iMapper = iMapper;
        }

        public async Task<DataTablePagination<ExpenseItemSearchDto>> Search(DataTablePagination<ExpenseItemSearchDto> searchDto)
        {
            var searchResult = Context.ExpenseItems.AsNoTracking();

            var searchModel = searchDto.SearchVm;
            var filter = searchDto?.Search?.Value?.Trim();

            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower();
                searchResult = searchResult.Where(c =>
                    c.Name.ToLower().Contains(filter)
                    || c.Description.ToLower().Contains(filter)
                );
            }

            var pageSize = searchDto.Length ?? 0;
            var skip = searchDto.Start ?? 0;

            var totalRecords = await searchResult.CountAsync();
            if (totalRecords <= 0) return searchDto;

            searchDto.RecordsTotal = totalRecords;
            searchDto.RecordsFiltered = totalRecords;

            var filteredDataList = await searchResult.OrderByDescending(c => c.Id).Skip(skip).Take(pageSize).ToListAsync();
            searchDto.Data = _iMapper.Map<List<ExpenseItemSearchDto>>(filteredDataList);

            var sl = searchDto.Start ?? 0;
            foreach (var searchResultDto in searchDto.Data)
            {
                var modelData = filteredDataList?.FirstOrDefault(c => c.Id == searchResultDto.Id);
                searchResultDto.SerialNo = ++sl;
                searchResultDto.Description = AppUtility.NullToDash(modelData?.Description);
            }

            return searchDto;
        }
    }
}
