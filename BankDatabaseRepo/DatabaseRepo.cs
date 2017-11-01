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



    }
}
