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
            //builder.Property(c => c.Question).HasMaxLength(FAPILength.GeneralText200Length);
            //builder.Property(c => c.Answer).HasMaxLength(FAPILength.Description1000Length);
          //  builder.ToTable("ExpenseItem");
        }
    }
}