using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LilliesDemo.Models;
using LilliesDemo.ViewModels;

namespace LilliesDemo
{
    public class BusinessLogicLayer
    {
        private DataAccessLayer dal;

        public BusinessLogicLayer()
        {
            dal = new DataAccessLayer();
        }

        public BusinessLogicLayer(LilliesContext context)
        {
            dal = new DataAccessLayer(context);
        }

        public async Task<bool> AddCustomer(CustomerViewModel customer)
        {
            if (await dal.Context.Customers.AnyAsync(m => m.Name == customer.Name))
                return false;

            Customer newCustomer = new Customer
            {
                Name = customer.Name,
                Address = customer.Address,
                PhoneNumber = customer.PhoneNumber
            };

            await dal.CreateCustomer(newCustomer);
            return true;
        }

        public async Task<bool> AddDelivery(DeliveryViewModel delivery)
        {
            int lillyCount = 0;
            if (!int.TryParse(delivery.LilliesDelivered, out lillyCount))
                return false;
            else if (lillyCount == 0)
                return false;

            Delivery newDelivery = new Delivery
            {
                CustomerID = delivery.CustomerID,
                DeliveryDate = delivery.DeliveryDate ?? DateTime.UtcNow,
                LilliesDelivered = lillyCount,
                Note = delivery.Note
            };

            await dal.CreateDelivery(newDelivery);
            return true;
        }

        public async Task<Dictionary<string, CustomerViewModel>> PopulateCustomerBox()
        {
            List<CustomerViewModel> customers = new List<CustomerViewModel>();

            customers = await dal.GetCustomersAsync((x) => true);
            return customers.ToDictionary(m => m.Name, m => m);

        }

        public async Task<Dictionary<string, DeliveryViewModel>> PopulateDeliveryBox(int customerID)
        {
            List<DeliveryViewModel> deliveries = new List<DeliveryViewModel>();

            deliveries = await dal.GetCustomerDeliveriesAsync((x) => x.CustomerID == customerID);
            return deliveries.Select((m, index) => new { index, m }).ToDictionary(c => String.Format("#{0} - {1}", c.index + 1, c.m.DeliveryDate.ToString()), c => c.m);
        }
    }
}
