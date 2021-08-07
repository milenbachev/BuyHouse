namespace BuyHouse.Services.Home
{
    using BuyHouse.Models.Home;
    using System.Collections.Generic;

    public interface IHomeService
    {
        IndexViewModel GetCounts();

        List<PropertyViewModel> GetLast();
    }
}
