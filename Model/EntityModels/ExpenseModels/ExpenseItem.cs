using System.ComponentModel;
using Model.ModelContracts;

namespace Model.EntityModels.ExpenseModels
{
    public class ExpenseItem : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }
        public string Description { get; set; }
    }
}
