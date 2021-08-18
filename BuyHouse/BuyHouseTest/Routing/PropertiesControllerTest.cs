namespace BuyHouseTest.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BuyHouse.Controllers;
    using BuyHouse.Models.Properties;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class PropertiesControllerTest
    {
        [Fact]
        public void GetAddShouldMapper() 
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Properties/Add")
                .To<PropertiesController>(x => x.Add());
        }

        [Fact]
        public void PostAddShouldMapper() 
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Properties/Add")
                    .WithMethod(HttpMethod.Post))
                .To<PropertiesController>(x => x.Add(With.Any<PropertyFormModel>()));
        }
        [Fact]
        public void GetDetailsShouldBeMapped()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Properties/Details/1")
                .To<PropertiesController>(x => x.Details(1));
        }

        [Fact]
        public void GetEditShouldMapped() 
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Properties/Edit/1")
                .To<PropertiesController>(x => x.Edit(1));
        }

        [Fact]
        public void PostEditShouldMapper() 
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Properties/Edit/1")
                    .WithMethod(HttpMethod.Post))
                .To<PropertiesController>(x => x.Edit(With.Any<int>(), With.Any<PropertyFormModel>()));
        }

        [Fact]
        public void AgentPropertiesShouldMapper()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Properties/AgentProperties")
                .To<PropertiesController>(x => x.AgentProperties());
        }

        [Fact]
        public void CurentAgentShouldMapper() 
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Properties/CurentAgent")
                .To<PropertiesController>(x => x.CurentAgent(With.Any<int>()));
        }
    }
}
