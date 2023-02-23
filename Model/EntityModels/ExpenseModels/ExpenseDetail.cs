using Model.ModelContracts;

namespace Model.EntityModels.ExpenseModels
{
    public class ExpenseDetail : IEntity
    {
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public int ExpenseItemId { get; set; }
        public double Qty { get; set; }
        public double UnitPrice { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public string Note { get; set; }

        public virtual Expense Expense { get; set; }
        public virtual ExpenseItem ExpenseItem { get; set; }

    }
}
