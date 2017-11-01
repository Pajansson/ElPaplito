using System;
using System.Collections.Generic;
using System.Text;

namespace BankLib
{
    public class Account
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
    }
}
