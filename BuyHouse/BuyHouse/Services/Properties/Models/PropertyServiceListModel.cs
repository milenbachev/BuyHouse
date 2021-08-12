namespace BuyHouse.Services.Properties.Models
{
    public class PropertyServiceListModel 
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public string ImageUrl { get; set; }
      
        public int Price { get; set; }

        public int Area { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string City { get; set; }

        public string Construction { get; set; }

        public string TypeOfTransaction { get; set; }

        public bool IsPublic { get; set; }
    }
}
