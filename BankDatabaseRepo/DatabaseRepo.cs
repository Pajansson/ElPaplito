using BankLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public void ImportAllData()
        {
            List<string> list = new List<string>();
            using (StreamReader reader = new StreamReader("bankdata-small.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
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
                    file.WriteLine($"{line.AccountId};{line.AccountId};{line.Balance}");
                }

            }
        }


    }
}
