using Model.ModelContracts;
using System;
using System.Collections.Generic;

namespace Model.EntityModels.ExpenseModels
{
    public class Expense : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ICollection<ExpenseDetail> Details { get; set; } = new List<ExpenseDetail>();
    }
}
