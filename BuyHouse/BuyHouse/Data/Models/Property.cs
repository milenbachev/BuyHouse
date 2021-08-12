namespace BuyHouse.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstant.Property;

    public class Property
    {
        public Property() 
        {
            this.Issues = new HashSet<Issue>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        public int Area { get; set; }

        public bool IsPublic { get; set; }

        public int? Floor { get; set; }
        
        public int? Floors { get; set; }

        public int? BedRoom { get; set; }

        public int? Bath { get; set; }

        public int Price { get; set; }

        public int Year { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        [MaxLength(CityNameMaxLength)]
        public string City { get; set; }

        [Required]
        [MaxLength(ConstructionMaxLength)]
        public string Construction { get; set; }

        public int AgentId { get; set; }

        public Agent Agent { get; set; }

        [Required]
        [MaxLength(TypeOfTransactionMaxLength)]
        public string TypeOfTransaction { get; set; }

        public IEnumerable<Issue> Issues { get; set; }

    }
}
