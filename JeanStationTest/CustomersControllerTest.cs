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

namespace JeanStationAPI.Tests
{
    [TestFixture]
    public class CustomersControllerTests
    {
      
        CustomersController controller = new CustomersController(new JeanStationContext(),null);
        Customer customer = new Customer
            {
               
                CustName = "John Doe",
                Address = "123 Main St",
                PhoneNo = "1234567890",
                Email = "john.doe@example.com",
                UserName = "johndoe",
                Pwd = "securepassword"
                };
        
      


        [Test]
        public async Task GetCustomersPositive()
        {
            var result = await controller.GetCustomers();
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Customer>>>(result);
        }
        [Test]
        public async Task GetCustomerPositive()
        {
            var result = await controller.GetCustomer(1001);
            Assert.IsAssignableFrom<ActionResult<Customer>>(result);

        }

        [Test]
        public async Task GetCustomerNegative()
        {
            var result = await controller.GetCustomer(200);
            Assert.IsAssignableFrom<ActionResult<Customer>>(result);
        }
        [Test]
        public async Task AddCustomerPositive()
        {
            var result = await controller.PostCustomer(customer);
            Assert.IsAssignableFrom<ActionResult<Customer>>(result);
        }
        [Test]
        public async Task AddCustomerNegative()
        {
            
            var result = await controller.PostCustomer(null);
            Assert.IsAssignableFrom<ActionResult<Customer>>(result);

        }
       

    }
}
