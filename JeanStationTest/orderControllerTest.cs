using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq;
using JeanStationAPI.Controllers;
using JeanStationAPI.Models;


namespace orderControllerTest
{
    [TestFixture]
    public class Tests
    {
        OrdersController controller = new OrdersController(new JeanStationContext());
        [Test]
        public async Task GetOrdersPositive()
        {

            var result = await controller.GetOrders();
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Order>>>(result);
        }

        [Test]
        public async Task GetOrdersWithId()
        {
            var result = await controller.GetOrdersByOrderId(1);
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Order>>>(result);
        }

        [Test]

        public async Task GetOrdersWithStoreId()
        {
            var result = await controller.GetOrdersByStoreId(1);
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Order>>>(result);
        }

        [Test]
        public async Task GetOrder()
        {
            var result = await controller.GetOrder(1, "101");
            Assert.IsAssignableFrom<ActionResult<Order>>(result);
        }

        [Test]
        public async Task PostOrder()
        {
            var result = await controller.PostOrder(1);
            Assert.IsAssignableFrom<ActionResult<Order>>(result);
        }

        [Test]
        public async Task PostOrderNegative()
        {
            var result = await controller.PostOrder(-1);
            Assert.IsAssignableFrom<ActionResult<Order>>(result);
        }
    }
}