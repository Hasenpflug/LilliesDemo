using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LilliesDemo.Models;

namespace LilliesDemo
{
    public class LilliesContext : DbContext
    {
        public LilliesContext() : base("Spidy's Flowers Database") { } 

        public LilliesContext(string databaseName) : base(databaseName) { }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Delivery> Deliveries { get; set; }
    }
}
