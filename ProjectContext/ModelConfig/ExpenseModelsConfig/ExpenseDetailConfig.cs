using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;
using Model.EntityModels.ExpenseModels;

namespace ProjectContext.ModelConfig.ExpenseModelsConfig
{
    public class ExpenseDetailConfig : IEntityTypeConfiguration<ExpenseDetail>
    {

        public void Configure(EntityTypeBuilder<ExpenseDetail> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Note).HasMaxLength(PropertyLength.Description500Length);
            builder.ToTable(TableName.ExpenseDetail.ToString());
        }
    }
}