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
    public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
    {
        private LabTestDbContext Context => Db as LabTestDbContext;
        private readonly IMapper _iMapper;
        public ExpenseRepository(LabTestDbContext db, IMapper iMapper) : base(db)
        {
            _iMapper = iMapper;
        }

        public override async Task<Expense> GetByIdAsync(int id)
        {
            var result = await Context.Expenses.Include(c => c.Details).ThenInclude(c => c.ExpenseItem).FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public async Task<DataTablePagination<ExpenseSearchDto>> Search(DataTablePagination<ExpenseSearchDto> searchDto)
        {
            var searchResult = Context.Expenses.Include(c => c.Details).AsNoTracking();

            var searchModel = searchDto.SearchVm;
            var filter = searchDto?.Search?.Value?.Trim();
            if (searchModel?.FromDate != null)
            {
                searchResult = searchResult.Where(m => m.Date.Date >= searchModel.FromDate.Value.Date);
            }
            if (searchModel?.ToDate != null)
            {
                searchResult = searchResult.Where(m => m.Date.Date <= searchModel.FromDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower();
                searchResult = searchResult.Where(c =>
                    c.Title.ToLower().Contains(filter)
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
            searchDto.Data = _iMapper.Map<List<ExpenseSearchDto>>(filteredDataList);

            var sl = searchDto.Start ?? 0;
            foreach (var searchResultDto in searchDto.Data)
            {
                var modelData = filteredDataList?.FirstOrDefault(c => c.Id == searchResultDto.Id);
                searchResultDto.SerialNo = ++sl;
                searchResultDto.Description = AppUtility.NullToDash(modelData?.Description);
                searchResultDto.Date = AppUtility.DateTimeToView(modelData?.Date);
            }

            return searchDto;
        }
    }
}
