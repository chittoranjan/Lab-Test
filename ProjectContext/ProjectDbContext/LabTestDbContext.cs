using Microsoft.EntityFrameworkCore;
using Model.EntityModels.ExpenseModels;
using ProjectContext.ModelConfig;

namespace ProjectContext.ProjectDbContext
{
    public class LabTestDbContext:DbContext
    {
        private readonly DbContextOptions _context;
        public LabTestDbContext(DbContextOptions<LabTestDbContext> context) : base(context)
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
