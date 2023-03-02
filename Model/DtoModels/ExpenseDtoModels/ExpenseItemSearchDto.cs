using Model.DataTableModels;
using System.ComponentModel.DataAnnotations;

namespace Model.DtoModels.ExpenseDtoModels
{
    public class ExpenseItemSearchDto : BaseDataTableSearch
    {
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string Description { get; set; }
    }
}
