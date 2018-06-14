using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LilliesDemo.Models;
using LilliesDemo.ViewModels;

namespace LilliesDemo
{
    public class DataAccessLayer
    {
        public LilliesContext Context { get; set; }

        public DataAccessLayer()
        {
            Context = new LilliesContext();
        }

        public DataAccessLayer(LilliesContext context)
        {
            Context = context;
        }

        public async Task CreateCustomer(Customer newCustomer)
        {
            Context.Customers.Add(newCustomer);
            await Context.SaveChangesAsync();
        }

        public async Task CreateDelivery(Delivery newDelivery)
        {
            Context.Deliveries.Add(newDelivery);
            await Context.SaveChangesAsync();
        }

        public async Task<List<CustomerViewModel>> GetCustomersAsync(Expression<Func<Customer, bool>> predicate)
        {
            List<CustomerViewModel> models = new List<CustomerViewModel>();

            models = await Context.Customers.Where(predicate).Select(m => new CustomerViewModel
            {
                CustomerID = m.CustomerID,
                Address = m.Address,
                Name = m.Name,
                PhoneNumber = m.PhoneNumber
            }).ToListAsync();

            return models;
        }

        public async Task<List<DeliveryViewModel>> GetCustomerDeliveriesAsync(Expression<Func<Delivery, bool>> predicate)
        {
            List<DeliveryViewModel> models = new List<DeliveryViewModel>();

            models = await Context.Deliveries.Where(predicate).Select(m => new DeliveryViewModel
            {
                DeliveryDate = m.DeliveryDate,
                LilliesDelivered = m.LilliesDelivered.ToString(),
                Note = m.Note
            }).ToListAsync();

            return models;
        }
    }
}
