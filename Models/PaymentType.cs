using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFBangazon.Models
{
    public class PaymentType
{
    public int AccountNo { get; set; }
    public string type { get; set; }
    public string Nickname { get; set; }
    public Customer Customer { get; set; }
}
}
