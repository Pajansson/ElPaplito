using BankLib;
using System;
using System.Collections.Generic;

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
