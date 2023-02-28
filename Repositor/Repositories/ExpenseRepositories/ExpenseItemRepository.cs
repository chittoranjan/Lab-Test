using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.DataTableModels;
using Model.DtoModels.ExpenseDtoModels;
using Model.EntityModels.ExpenseModels;
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

        public async Task<DataTablePagination<ExpenseItemSearchDto, ExpenseItemSearchDto>> Search(DataTablePagination<ExpenseItemSearchDto, ExpenseItemSearchDto> searchDto)
        {
            var searchResult = Context.ExpenseItems.AsNoTracking();

            var searchModel = searchDto.SearchModel;
            var filter = !string.IsNullOrEmpty(searchDto?.Filter) ? searchDto?.Filter?.Trim() : searchDto.Filter?.Trim();

            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower();
                searchResult = searchResult.Where(c =>
                    c.Name.ToLower().Contains(filter)
                    || c.Description.ToLower().Contains(filter)
                );

            }

            var totalRecords = await searchResult.CountAsync();
            searchDto.TotalItemsCount = totalRecords;
            if (totalRecords <= 0) return searchDto;

            var filteredDataList = await searchResult.OrderByDescending(c => c.Id).Skip(searchDto.StartPoint).Take(searchDto.ItemsPerPage).ToListAsync();
            searchDto.DataList = _iMapper.Map<List<ExpenseItemSearchDto>>(filteredDataList);
            var sl = searchDto.StartPoint;

            foreach (var searchResultDto in searchDto.DataList)
            {
                searchResultDto.SerialNo = ++sl;
            }

            return searchDto;
        }
    }
}
