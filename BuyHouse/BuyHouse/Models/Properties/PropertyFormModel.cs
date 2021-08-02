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

        [Display(Name = "City")]
        public int CityId { get; set; }

        [Display(Name = "Construction")]
        public int ConstructionId { get; set; }

        [Display(Name = "Agent")]
        public int? AgentId { get; set; }

        [Display(Name = "Type of Transaction")]
        public int TypeOfTransactionId { get; set; }

        public IEnumerable<PropertyCategoryServiceModel> Categories { get; set; }

        public IEnumerable<PropertyTransactionServiceModel> Transactions { get; set; }

        public IEnumerable<PropertyCityServiceModel> Cities { get; set; }

        public IEnumerable<PropertyConstructionServiceModel> Constructions { get; set; }

    }
}
