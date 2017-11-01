﻿using BankLib;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public Tuple<List<Account>, List<Customer>> ImportAllData()
        {

            DirectoryInfo info = new DirectoryInfo(@"C:\Development\Bank\");
            FileInfo[] files = info.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();

            var bankInfo = files[0];

            var x = Path.GetFileName(bankInfo.ToString());

            string[] arr = System.IO.File.ReadAllLines(@"C:\Development\Bank\bankdata-small.txt");
            int lineCountOne = 0;
            int lineCountTwo = 0;

            foreach (var line in arr)
            {
                if (_customer.Count() == 0 && !line.Contains(";"))
                {
                    lineCountOne = Int32.Parse(line);
                    continue;
                }

                if (_customer.Count() == lineCountOne && !line.Contains(";"))
                {
                    lineCountTwo = Int32.Parse(line);
                    continue;

                }

                if (lineCountOne != _customer.Count())
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
                else if (lineCountTwo != _accounts.Count())
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

            var tuple = new Tuple<List<Account>, List<Customer>>(_accounts,_customer);

            return tuple;
        }


        public void SaveDateToTextFile()
        {
            DateTime time = DateTime.Now;

            time.ToString("yyyymmdd-hhmm");

            //using (System.IO.StreamWriter file = new System.IO.StreamWriter($"C:\\Development\\Bank\\WriteLines2.txt"))
            //{
            //    foreach(var line in _customer)
            //    {
            //            file.WriteLine(line);  
            //    }
            //}
        }


    }
}
