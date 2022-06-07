using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.EntityModels.ExpenseModels;

namespace ProjectContext.ModelConfig.ExpenseModelsConfig
{
    public class ExpenseItemConfig : IEntityTypeConfiguration<ExpenseItem>
    {

        public void Configure(EntityTypeBuilder<ExpenseItem> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(PropertyLength.Name100Length);
            builder.Property(c => c.Description).HasMaxLength(PropertyLength.Description500Length);
            builder.ToTable("ExpenseItem");
        }
    }
}