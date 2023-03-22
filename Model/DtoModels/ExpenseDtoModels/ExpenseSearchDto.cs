using Model.DataTableSearchModels;
using System.Collections.Generic;

namespace Model.DtoModels.ExpenseDtoModels
{
    public class ExpenseSearchDto : BaseDataTableSearch
    {
        public string Title { get; set; }

        //public DateTime Date { get; set; }
        public string Date { get; set; }

        public string Description { get; set; }

        public ICollection<ExpenseDetailDto> Details { get; set; } = new List<ExpenseDetailDto>();
    }
}
