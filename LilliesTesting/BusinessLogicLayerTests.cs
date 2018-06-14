using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LilliesDemo;
using LilliesDemo.Models;
using LilliesDemo.ViewModels;

namespace LilliesTesting
{
    [TestFixture]
    public class BusinessLogicLayerTests
    {
        BusinessLogicLayer controller;
        Mock<LilliesContext> mockContext;

        [OneTimeSetUp]
        public void TestSetup()
        {
            List<Customer> mockCustomers = new List<Customer>();
            List<Delivery> mockDeliveries = new List<Delivery>();
            Mock<DbSet<Customer>> mockCustomerSet = new Mock<DbSet<Customer>>();
            Mock<DbSet<Delivery>> mockDeliverySet = new Mock<DbSet<Delivery>>();

            mockCustomerSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Customer>(mockCustomers.AsQueryable().Provider));
            mockCustomerSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(mockCustomers.AsQueryable().Expression);
            mockCustomerSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(mockCustomers.AsQueryable().ElementType);
            mockCustomerSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(mockCustomers.GetEnumerator());
            mockCustomerSet.As<IDbAsyncEnumerable<Customer>>().Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Customer>(mockCustomers.GetEnumerator()));

            mockDeliverySet.As<IQueryable<Delivery>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Delivery>(mockDeliveries.AsQueryable().Provider));
            mockDeliverySet.As<IQueryable<Delivery>>().Setup(m => m.Expression).Returns(mockDeliveries.AsQueryable().Expression);
            mockDeliverySet.As<IQueryable<Delivery>>().Setup(m => m.ElementType).Returns(mockDeliveries.AsQueryable().ElementType);
            mockDeliverySet.As<IQueryable<Delivery>>().Setup(m => m.GetEnumerator()).Returns(mockDeliveries.GetEnumerator());
            mockDeliverySet.As<IDbAsyncEnumerable<Delivery>>().Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Delivery>(mockDeliveries.GetEnumerator()));

            mockCustomerSet.Setup(m => m.Add(It.IsAny<Customer>())).Callback<Customer>(m => mockCustomers.Add(m));
            mockDeliverySet.Setup(m => m.Add(It.IsAny<Delivery>())).Callback<Delivery>(m => mockDeliveries.Add(m));

            mockCustomerSet.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<Customer>>())).Callback<IEnumerable<Customer>>((customerList) => mockCustomers.RemoveAll((m) => true));
            mockDeliverySet.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<Delivery>>())).Callback<IEnumerable<Delivery>>((deliveryList) => mockDeliveries.RemoveAll((x) => true));


            mockContext = new Mock<LilliesContext>();
            mockContext.Setup(m => m.Customers).Returns(mockCustomerSet.Object);
            mockContext.Setup(m => m.Deliveries).Returns(mockDeliverySet.Object);
            controller = new BusinessLogicLayer(mockContext.Object);
        }

        [TearDown]
        public void ClearDataAfterTest()
        {
            mockContext.Object.Customers.RemoveRange(null);
            mockContext.Object.Deliveries.RemoveRange(null);
        }

        [Test]
        public async Task CannotAdd_Customer_UsingExistingName()
        {
            CustomerViewModel testCustomer = new CustomerViewModel { Name = "Braedon Hasen" };
            await controller.AddCustomer(testCustomer);

            Assert.That(!controller.AddCustomer(testCustomer).Result);
        }

        [Test]
        public async Task CannotAdd_Delivery_WithZeroLillies()
        {
            DeliveryViewModel testDelivery = new DeliveryViewModel { CustomerID = 1, LilliesDelivered = "0" };
            Assert.That(!(await controller.AddDelivery(testDelivery)));
        }
    }
}
