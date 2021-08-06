namespace BuyHouse.Models.Issues
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static BuyHouse.Data.DataConstant.Issue;

    public class AddIssueFormModel
    {
        [Required]
        [StringLength(DesccriptionMaxLength , MinimumLength = DescriptionMinLength)]
        public string DescriptionIssue { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
