using Model.DataTableSearchModels;
using System.ComponentModel;

namespace Model.DtoModels.ExpenseDtoModels
{
    public class ExpenseDetailSearchDto : BaseDataTableSearch
    {

        public int ExpenseId { get; set; }

        [DisplayName("Expense Title")]
        public string ExpenseTitle { get; set; }

        public int ExpenseItemId { get; set; }

        [DisplayName("Expense Item Name")]
        public string ExpenseItemName { get; set; }

        public double Qty { get; set; }

        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }

        public string Note { get; set; }

    }
}
