using System;
using System.Collections.Generic;
using System.Text;

namespace BankLib
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
