namespace BuyHouse.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstant.Construction;

    public class Construction
    {
        public Construction() 
        {
            this.Properties = new HashSet<Property>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Property> Properties { get; set; }
    }
}
