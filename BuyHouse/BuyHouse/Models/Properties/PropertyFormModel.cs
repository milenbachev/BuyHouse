namespace BuyHouse.Models.Properties
{
    using BuyHouse.Services.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstant.Property;

    public class PropertyFormModel
    {
        [Range(AreaMinValue, AreaMaxValue)]
        public int Area { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; }

        [Range(YearMinValue, YearMaxValue)]
        public int Year { get; set; }

        public int? Floor { get; set; }

        public int? Floors { get; set; }

        public int? BedRoom { get; set; }

        public int? Bath { get; set; }

        [Range(PriceMinValue, PriceMaxValue)]
        public int Price { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [Display(Name = "Category")]
        public int CategotyId { get; set; }

        [Required]
        [StringLength(CityNameMaxLength, MinimumLength = CityNameMinLength)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [StringLength(ConstructionMaxLength, MinimumLength = ConstructionMinLength)]
        [Display(Name = "Construction")]
        public string Construction { get; set; }

        [Display(Name = "Agent")]
        public int? AgentId { get; set; }

        [Required]
        [StringLength(TypeOfTransactionMaxLength, MinimumLength = TypeOfTransactionMinLength)]
        [Display(Name = "Type of Transaction")]
        public string TypeOfTransaction { get; set; }

        public IEnumerable<PropertyCategoryServiceModel> Categories { get; set; }

    }
}
