using Model.DataTableModels;
using System.ComponentModel;

namespace Model.DtoModels.ExpenseDtoModels
{
    public class ExpenseItemSearchDto : BaseDataTableSearch
    {
        public string Name { get; set; }
        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }
        public string Description { get; set; }
    }
}
