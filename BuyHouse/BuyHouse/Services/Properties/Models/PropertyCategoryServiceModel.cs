namespace BuyHouse.Services.Properties
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstant.Category;
    public class PropertyCategoryServiceModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}
