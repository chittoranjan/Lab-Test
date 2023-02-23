using Model.EntityModels.ExpenseModels;
using ProjectContext.ProjectDbContext;
using Repository.BaseRepository;
using Repository.IRepositories.IExpenseRepositories;

namespace Repository.Repositories.ExpenseRepositories
{
    public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
    {
        private LabTestDbContext Context => Db as LabTestDbContext;
        public ExpenseRepository(LabTestDbContext db) : base(db)
        {
        }
    }
}
