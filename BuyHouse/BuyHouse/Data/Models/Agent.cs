namespace BuyHouse.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstant.Agent;

    public class Agent
    {
        public Agent() 
        {
            this.Properties = new HashSet<Property>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public IEnumerable<Property> Properties { get; set; }
    }
}
