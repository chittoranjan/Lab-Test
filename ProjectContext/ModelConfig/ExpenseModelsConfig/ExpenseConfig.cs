using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;
using Model.EntityModels.ExpenseModels;

namespace ProjectContext.ModelConfig.ExpenseModelsConfig
{
    public class ExpenseConfig : IEntityTypeConfiguration<Expense>
    {

        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).HasMaxLength(PropertyLength.GeneralText200Length);
            builder.Property(c => c.Description).HasMaxLength(PropertyLength.Description500Length);
            builder.ToTable(TableName.Expense.ToString());
        }
    }
}