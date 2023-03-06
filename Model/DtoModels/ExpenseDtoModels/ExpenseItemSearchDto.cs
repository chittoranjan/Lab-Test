using System.ComponentModel;
using Model.DataTableSearchModels;

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
