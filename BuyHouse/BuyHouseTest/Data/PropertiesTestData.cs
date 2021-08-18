namespace BuyHouseTest.Data
{
    using BuyHouse.Data.Models;
    public static class PropertiesTestData
    {
        public static bool IsAgent()
        {
            var user = new User
            {
                IsAgent = true
            };

            return user.IsAgent == true;
        }
    }
}
