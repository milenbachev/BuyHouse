namespace BuyHouse.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstant.Property;

    public class Property
    {
        public int Id { get; set; }

        public int Area { get; set; }

        public int? Floor { get; set; }
        
        public int? Floors { get; set; }

        public int? BedRoom { get; set; }

        public int? Bath { get; set; }

        public int Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public int CategotyId { get; set; }

        public Category Category { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public int ConstructionId { get; set; }

        public Construction Construction { get; set; }

        public int? AgentId { get; set; }

        public Agent Agent { get; set; }

        public int TypeOfTransactionId { get; set; }

        public TypeOfTransaction TypeOfTransaction { get; set; }
    }
}
