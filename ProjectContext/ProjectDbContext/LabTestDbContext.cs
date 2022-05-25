using Microsoft.EntityFrameworkCore;
using Model.EntityModels.ExpenseModels;
using ProjectContext.ModelConfig;

namespace Lab_Test.Models
{
    public class LabTestDbContext:DbContext
    {
        private readonly DbContextOptions _context;
        public LabTestDbContext(DbContextOptions context) : base(context)
        {
            _context = context;
        }
        public DbSet<ExpenseItem> ExpenseItems { get; set; }


        #region Model API Config
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new BaseModelConfig().ModelBuilderConfig(modelBuilder);

        }
        #endregion
    }
}
