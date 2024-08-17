using JeanStationAPI.Controllers;
using JeanStationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeanStationTest
{
    [TestFixture]
    internal class CartsControllerTest
    {
        CartsController controller = new CartsController(new JeanStationContext());
        Cart cart = new Cart
        {


            /*CustName = "John Doe",
            Address = "123 Main St",
            PhoneNo = "1234567890",
            Email = "john.doe@example.com",
            UserName = "johndoe",
            Pwd = "securepassword"*/
        };




        [Test]
        public async Task GetCustomersPositive()
        {
            var result = await controller.GetCarts();
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Cart>>>(result);
        }
        [Test]
        public async Task GetCartByCustomerIdPositive()
        {
            var result = await controller.GetCartByCustomerId(1001);
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Cart>>>(result);

        }

        [Test]
        public async Task GetCartByCustomerIdNegative()
        {
            var result = await controller.GetCartByCustomerId(200);
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Cart>>>(result);
        }
        
    }
}
