using System;
using System.Collections.Generic;

namespace wallet.Models
{
 
   
    public class Account
    { 
        public String Currency { get; set; }
        public decimal Sum { get; set; } = 0;
    }
}