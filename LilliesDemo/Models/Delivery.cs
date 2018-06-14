using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilliesDemo.Models
{
    public class Delivery
    {
        [Key]
        public int DeliveryID { get; set; }

        public int CustomerID { get; set; }

        public int LilliesDelivered { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string Note { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
