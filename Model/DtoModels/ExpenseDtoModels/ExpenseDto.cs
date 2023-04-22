
using Model.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public List<ExpenseDetailDto> GetCheckedItemSameOrNot()
        {
            var checkedDetailItems = new List<ExpenseDetailDto>();
            foreach (var dt in Details)
            {

                var existDetail = checkedDetailItems.FirstOrDefault(c => c.ExpenseItemId == dt.ExpenseItemId);
                if (existDetail != null) { existDetail.Qty += dt.Qty; existDetail.ExpenseId = Id; }
                else { dt.ExpenseId = Id; checkedDetailItems.Add(dt); }
            }

            return checkedDetailItems;
        }
    }
}
