using AutoMapper;
using Model.EntityModels.ExpenseModels;
using ProjectContext.ProjectDbContext;
using Repository.BaseRepository;
using Repository.IRepositories.IExpenseRepositories;

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
    }
}
