namespace BuyHouse.Services.Properties
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstant.TypeOfTransaction;

    public class PropertyTransactionServiceModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}
