using BankLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Transaction = BankLib.Transaction;


namespace BankLogicRepo
{
    public class BankLogic
    {
        public string Transaction(Transaction transaction)
        {
            var accounts = new List<Account>(); //todo get data
            var fromAcc = accounts.FirstOrDefault(x => x.AccountId == transaction.FromAccountId);
            var toAcc = accounts.FirstOrDefault(x => x.AccountId == transaction.ToAccountId);
            var result = "";

            if (CheckIfTransactionPossible(transaction))
            {
                fromAcc.Balance = -transaction.Amount;
                toAcc.Balance = +transaction.Amount;
                result = "Success";
            }
            else
            {
                result = "Lol you are too poor ";
            }
            return result;
        }

        public bool CheckIfTransactionPossible(Transaction transaction)
        {
            var accounts = new List<Account>(); //todo get data
            var fromAcc = accounts.FirstOrDefault(x => x.AccountId == transaction.FromAccountId);

            if (fromAcc.Balance <= transaction.Amount && transaction.Amount > 0)
            {
                return false;
            }
            return true;
        }

        public void Deposit(decimal amount, Account account)
        {
            if (amount > 0)
            {
                account.Balance =+ amount;
            }
        }

        public void Withdraw(decimal amount, Account account)
        {
            if (amount > 0)
            {
                account.Balance =- amount;
            }
        }


        public void Search(string searchInput)
        {
            //todo
        }
    }
}