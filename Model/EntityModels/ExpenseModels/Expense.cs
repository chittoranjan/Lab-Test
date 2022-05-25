using System;
using System.Collections.Generic;
using Model.ModelContracts;

namespace Model.EntityModels.ExpenseModels
{
    public class Expense:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ICollection<ExpenseDetail> Details { get; set; } = new List<ExpenseDetail>();
    }
}
