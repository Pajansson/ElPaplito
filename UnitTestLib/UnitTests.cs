using BankLib;
using System;
using Xunit;

namespace UnitTestLib
{
    public class UnitTest1
    {
        public bool CheckIfTransactionPossible(Account fromAcc, decimal amount)
        {
            if (fromAcc.Balance <= amount && amount > 0)
            {
                return false;
            }
            return true;
        }

        [Fact]
        public void TransactionTest()
        {
            Account fromAcc = new Account()
            {
                AccountId = 12345,
                CustomerId = 1001,
                Balance = 3000.50M
            };
            Account toAcc = new Account()
            {
                AccountId = 67890,
                CustomerId = 1002,
                Balance = 1000.50M
            };
            decimal amount = 1500;

            if (CheckIfTransactionPossible(fromAcc, amount))
            {
                fromAcc.Balance -= amount;
                toAcc.Balance += amount;
                Transaction transaction = new Transaction()
                {
                    Amount = amount,
                    FromAccountId = fromAcc.AccountId,
                    ToAccountId = toAcc.AccountId
                };
            }
            Assert.Equal(1500.50M, fromAcc.Balance);        
        }
    }
}
