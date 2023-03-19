
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.DtoModels.ExpenseDtoModels
{
    public class ExpenseDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(PropertyLength.GeneralText200Length)]
        public string Title { get; set; }

        public DateTime Date { get; set; }

        [StringLength(PropertyLength.Description500Length)]
        public string Description { get; set; }

        public ICollection<ExpenseDetailDto> Details { get; set; } = new List<ExpenseDetailDto>();
    }
}
