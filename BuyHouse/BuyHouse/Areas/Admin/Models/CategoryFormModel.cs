namespace BuyHouse.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstant.Category;
    public class CategoryFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}
