using System;
using System.Collections.Generic;

namespace Model.DtoModels.ExpenseDtoModels
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ICollection<ExpenseDetailDto> Details { get; set; } = new List<ExpenseDetailDto>();
    }
}
