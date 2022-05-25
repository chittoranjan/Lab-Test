using Lab_Test.Models;
using Model.EntityModels.ExpenseModels;
using Repository.BaseRepository;
using Repository.IRepositories;

namespace Repository.Repositories
{
    public class ExpenseItemRepository : BaseRepository<ExpenseItem>, IExpenseItemRepository
    {
        private LabTestDbContext Context => Db as LabTestDbContext;
        public ExpenseItemRepository(LabTestDbContext db) : base(db)
        {
        }
    }
}
