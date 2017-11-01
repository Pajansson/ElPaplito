﻿using BankLib;
using System;
using System.Collections.Generic;
using System.IO;

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

    }
}
