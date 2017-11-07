using BankLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BankLogicRepo;
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

        [Fact]
        public void TransactionWithNegativeNumber()
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
            decimal amount = -1500;

            if (CheckIfTransactionPossible(fromAcc, amount))
            {
                if (amount > 0)
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

            }

            Assert.Equal(3000.50M, fromAcc.Balance);
        }

        [Fact]
        public void FileExists()
        {
            DirectoryInfo info = new DirectoryInfo(@"C:\Development\Bank\");
            FileInfo[] files = info.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();

            var bankInfo = files[0];
            var fileName = Path.GetFileName(bankInfo.ToString());

            Assert.NotEqual("TestValue", fileName);

        }

        [Fact]
        public void FileHasValue() 
        {
            DirectoryInfo info = new DirectoryInfo(@"C:\Development\Bank\");
            FileInfo[] files = info.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();

            var bankInfo = files[0];
            var x = Path.GetFileName(bankInfo.ToString());

            string[] arr = System.IO.File.ReadAllLines($"C:\\Development\\Bank\\{x}");

            Assert.NotEqual(0, arr.Length);
        }

        [Fact]
        public void GetCustomerAccount()
        {
            var bankLogic = new BankLogic();
            var accounts = new List<Account>();
            var acc1 = new Account
            {
                Balance = 1111,
                AccountId = 1,
                CustomerId = 1
            };
            var acc2 = new Account
            {
                Balance = 3333,
                AccountId = 2,
                CustomerId = 1
            };
            var acc3 = new Account
            {
                Balance = 444,
                AccountId = 3,
                CustomerId = 3
            };
            accounts.Add(acc1);
            accounts.Add(acc2);
            accounts.Add(acc3);
            var result = bankLogic.GetCustomersAccounts(1, accounts).Count;
            Assert.Equal(2, result);
        }
    }
}
