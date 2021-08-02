namespace BuyHouse.Services.Properties
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstant.Construction;

    public class PropertyConstructionServiceModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}
