using Model.EntityModels.ExpenseModels;
using ProjectContext.ProjectDbContext;
using Repository.BaseRepository;
using Repository.IRepositories.IExpenseRepositories;

namespace Repository.Repositories.ExpenseRepositories
{
    public class ExpenseDetailRepository : BaseRepository<ExpenseDetail>, IExpenseDetailRepository
    {
        private LabTestDbContext Context => Db as LabTestDbContext;
        public ExpenseDetailRepository(LabTestDbContext db) : base(db)
        {
        }
    }
}
