using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilliesDemo.ViewModels
{
    public class DeliveryViewModel
    { 
        public int CustomerID { get; set; }

        public string LilliesDelivered { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string Note { get; set; }

    }
}
