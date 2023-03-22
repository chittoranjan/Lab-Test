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
    public class ExpenseDetailRepository : BaseRepository<ExpenseDetail>, IExpenseDetailRepository
    {
        private LabTestDbContext Context => Db as LabTestDbContext;
        private readonly IMapper _iMapper;
        public ExpenseDetailRepository(LabTestDbContext db, IMapper iMapper) : base(db)
        {
            _iMapper = iMapper;
        }

        public async Task<DataTablePagination<ExpenseDetailSearchDto>> Search(DataTablePagination<ExpenseDetailSearchDto> searchDto)
        {
            var searchResult = Context.ExpenseDetails.Include(c => c.ExpenseItem).Include(c => c.Expense).AsNoTracking();

            var searchModel = searchDto.SearchVm;
            var filter = searchDto?.Search?.Value?.Trim();

            if (searchModel.ExpenseId > 0)
            {
                searchResult = searchResult.Where(m => m.ExpenseId == searchModel.ExpenseId);
            }
            if (searchModel.ExpenseItemId > 0)
            {
                searchResult = searchResult.Where(m => m.ExpenseItemId == searchModel.ExpenseItemId);
            }

            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower();
                searchResult = searchResult.Where(c =>
                    c.Expense.Title.ToLower().Contains(filter)
                    || c.ExpenseItem.Name.ToLower().Contains(filter)
                    || c.Note.ToLower().Contains(filter)
                    || c.Qty.ToString().Contains(filter)
                    || c.UnitPrice.ToString().Contains(filter)
                    || c.Price.ToString().Contains(filter)
                    || c.Discount.ToString().Contains(filter)
                );
            }

            var pageSize = searchDto.Length ?? 0;
            var skip = searchDto.Start ?? 0;

            var totalRecords = await searchResult.CountAsync();
            if (totalRecords <= 0) return searchDto;

            searchDto.RecordsTotal = totalRecords;
            searchDto.RecordsFiltered = totalRecords;

            var filteredDataList = await searchResult.OrderByDescending(c => c.Id).Skip(skip).Take(pageSize).ToListAsync();
            searchDto.Data = _iMapper.Map<List<ExpenseDetailSearchDto>>(filteredDataList);

            var sl = searchDto.Start ?? 0;
            foreach (var searchResultDto in searchDto.Data)
            {
                var modelData = filteredDataList?.FirstOrDefault(c => c.Id == searchResultDto.Id);
                searchResultDto.SerialNo = ++sl;
                searchResultDto.ExpenseTitle = AppUtility.NullToDash(modelData?.Expense?.Title);
                searchResultDto.ExpenseItemName = AppUtility.NullToDash(modelData?.ExpenseItem?.Name);
                searchResultDto.Note = AppUtility.NullToDash(modelData?.Note);
            }

            return searchDto;
        }
    }
}
