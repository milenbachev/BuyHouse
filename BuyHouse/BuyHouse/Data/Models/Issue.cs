namespace BuyHouse.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstant.Issue;
    public class Issue
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }

        public Property Property { get; set; }

        public string UserId { get; set; }

        [Required]
        [MaxLength(DesccriptionMaxLength)]
        public string DescriptionIssue { get; set; }

        public DateTime CreateOn { get; set; }
             
    }
}
