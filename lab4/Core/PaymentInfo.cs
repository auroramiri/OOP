using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4.Core
{
    public class PaymentInfo
    {
        public PaymentData PaymentData { get; set; }
        public ObservableCollection<Product> ShoppingList { get; set; }
    }
}
