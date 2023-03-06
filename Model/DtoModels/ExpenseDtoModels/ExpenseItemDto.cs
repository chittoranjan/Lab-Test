using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.DtoModels.ExpenseDtoModels
{
    public class ExpenseItemDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(PropertyLength.Name100Length)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }

        [StringLength(PropertyLength.Description500Length)]
        public string Description { get; set; }
    }
}
