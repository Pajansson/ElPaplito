using BankLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace BankDatabaseRepo
{

    public class DatabaseRepo
    {

        private List<Account> _accounts;
        private List<Customer> _customer;
        private List<Transaction> _transaction;

        public DatabaseRepo()
        {
            _accounts = new List<Account>();
            _customer = new List<Customer>();
            _transaction = new List<Transaction>();
        }

        public Tuple<List<Account>, List<Customer>> ImportAllData()
        {
            DirectoryInfo info = new DirectoryInfo(@"C:\Development\Bank\");
            FileInfo[] files = info.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();

            var bankInfo = files[0];
            var x = Path.GetFileName(bankInfo.ToString());

            string[] arr = System.IO.File.ReadAllLines($"C:\\Development\\Bank\\{x}");

            foreach (var line in arr)
            {
                var items = line.Split(';');
                if (items.Length == 9)
                {
                    //Add to customer.
                    Customer customer = new Customer();
                    string[] parts = line.Split(';');

                    customer.CustomerId = Int32.Parse(parts[0]);
                    customer.OrginisationNumber = parts[1];
                    customer.Name = parts[2];
                    customer.Adress = parts[3];
                    customer.City = parts[4];
                    customer.State = parts[5];
                    customer.ZipCode = parts[6];
                    customer.Country = parts[7];
                    customer.Phone = parts[8];

                    _customer.Add(customer);
                }
                else if (items.Length == 3)
                {
                    //Add to account.
                    Account account = new Account();
                    string[] parts = line.Split(';');

                    account.AccountId = Int32.Parse(parts[0]);
                    account.CustomerId = Int32.Parse(parts[1]);
                    account.Balance = Decimal.Parse(parts[2], CultureInfo.InvariantCulture);

                    _accounts.Add(account);
                }
            }
            var tuple = new Tuple<List<Account>, List<Customer>>(_accounts, _customer);
            return tuple;
        }


        public void SaveDateToTextFile()
        {
            DateTime time = DateTime.Now;

            using (System.IO.StreamWriter file = new System.IO.StreamWriter($"C:\\Development\\Bank\\{time.ToString("yyyyMMdd-HHmm")}.txt"))
            {
                file.WriteLine(_customer.Count().ToString());
                foreach (var line in _customer)
                {
                    file.WriteLine($"{line.CustomerId};{line.OrginisationNumber};{line.Name};{line.Adress};{line.City};{line.State};{line.ZipCode};{line.Country};{line.Phone}");
                }
                file.WriteLine(_accounts.Count().ToString());
                foreach (var line in _accounts)
                {
                    file.WriteLine($"{line.AccountId};{line.AccountId};{line.Balance.ToString().Replace(',', '.')}");
                }

            }
        }


        public List<Account> AllAccounts()
        {
            return _accounts;
        }

        public List<Transaction> AllTransactions()
        {
            return _transaction;
        }

        public List<Customer> AllCustomers()
        {
            return _customer;
        }

        public bool CreateTransaction(Transaction transaction)
        {
            try
            {
                transaction.TransactionId = _transaction.Count() + 1;
                _transaction.Add(transaction);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
