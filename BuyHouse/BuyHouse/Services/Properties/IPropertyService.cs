namespace BuyHouse.Services.Properties
{
    using BuyHouse.Services.Properties.Models;
    using System.Collections.Generic;
    public interface IPropertyService
    {
        int Create(
            int area,
            string title,
            int year,
            int? floor,
            int? floors,
            int? bedRoom,
            int? bath,
            int price,
            string imageUrl,
            string description,
            int categoryId,
            string typeOfTransaction,
            string city,
            string construction,
            int? agentId);

        PropertyServiceQueryModel All(
            string transaction,
            string city,
            string construction,
            int curentPage, 
            int propertyPerPage);

        PropertyDetailsServiceModel Details(int id);

        bool Edit(
            int id,
            int area,
            string title,
            int year,
            int? floor,
            int? floors,
            int? bedRoom,
            int? bath,
            int price,
            string imageUrl,
            string description,
            int categoryId,
            string typeOfTransaction,
            string city,
            string construction);

        bool Delete(int id);

        IEnumerable<PropertyServiceListModel> AgentProperties(string userId);

        IEnumerable<PropertyServiceListModel> CurentAgentProperties(int? id);

        IEnumerable<PropertyCategoryServiceModel> AllCategory();

        bool CategoryExist(int categoryId);

        IEnumerable<string> AllCity();

        IEnumerable<string> AllConstruction();

        IEnumerable<string> AllTransaction();

        bool AgentIsCreate(int propertyId, int agentId);
    }
}
