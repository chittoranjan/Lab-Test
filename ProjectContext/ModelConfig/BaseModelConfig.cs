using Microsoft.EntityFrameworkCore;
using Model.EntityModels.ExpenseModels;
using ProjectContext.ModelConfig.ExpenseModelsConfig;

namespace ProjectContext.ModelConfig
{
    public class BaseModelConfig
    {
        public void ModelBuilderConfig(ModelBuilder builder)
        {
            builder.HasDefaultSchema("dbo");

            new ExpenseItemConfig()?.Configure(builder.Entity<ExpenseItem>());
            new ExpenseConfig()?.Configure(builder.Entity<Expense>());
            new ExpenseDetailConfig()?.Configure(builder.Entity<ExpenseDetail>());
        }
    }
}
