using Microsoft.EntityFrameworkCore;

namespace Lab_Test.Models
{
    public class LabTestContext:DbContext
    {
        private readonly DbContextOptions _context;
        public LabTestContext(DbContextOptions context) : base(context)
        {
            _context = context;
        }
        public DbSet<ExpenseItem> ExpenseItems { get; set; }
    }
}
