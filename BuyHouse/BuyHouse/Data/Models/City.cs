namespace BuyHouse.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstant.City;

    public class City
    {
        public City() 
        {
            this.Properties = new HashSet<Property>();
            this.Agents = new HashSet<Agent>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Property> Properties { get; set; }

        public IEnumerable<Agent> Agents { get; set; }
    }
}
