using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4.Core
{
    public class PaymentData
    {
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string ExpireDate { get; set; }
        public string CVV { get; set; }
        public string TotalPrice { get; set; }
        public int ProductQuantity { get; set; }
    }
}
