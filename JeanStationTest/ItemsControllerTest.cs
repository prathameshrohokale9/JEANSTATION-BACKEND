using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq;
using JeanStationAPI.Controllers;
using JeanStationAPI.Models;
using Castle.Core.Logging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;

namespace JeanStationTest
{
    [TestFixture]
    internal class ItemsControllerTest
    {
        ItemsController controller = new ItemsController(new JeanStationContext());
        Item item = new Item
        {
            ItemCode = "A0007",
            ItemName = "Blue Jacket",
            Price = 1000,
            Quantity = 10,
            StoreId = 2,
            ItemImage = "https://rukminim2.flixcart.com/image/612/612/xif0q/jacket/h/g/v/s-1-no-jk-302-reya-original-imagvhg2sffpm9q7.jpeg?q=70"
            
        };




        [Test]
        public async Task GetItemsPositive()
        {
            var result = await controller.GetItems();
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Item>>>(result);
        }
        [Test]
        public async Task GetItemPositive()
        {
            var result = await controller.GetItem("A0009");
            Assert.IsAssignableFrom<ActionResult<Item>>(result);

        }

        [Test]
        public async Task GetItemNegative()
        {
            var result = await controller.GetItem("");
            Assert.IsAssignableFrom<ActionResult<Item>>(result);
        }
        [Test]
        public async Task AddItemPositive()
        {
            var result = await controller.PostItem(item);
            Assert.IsAssignableFrom<ActionResult<Item>>(result);
        }
        [Test]
        public async Task AddItemNegative()
        {

            var result = await controller.PostItem(null);
            Assert.IsAssignableFrom<ActionResult<Item>>(result);

        }
    }
}
