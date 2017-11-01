using BankLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankLogicRepo
{
    public class BankLogic
    {
        public string Transaction(int fromAccId, int toAccId, decimal amount)
        {
            var accounts = new List<Account>(); //todo get data
            var fromAcc = accounts.FirstOrDefault(x => x.AccountId == fromAccId);
            var toAcc = accounts.FirstOrDefault(x => x.AccountId == toAccId);
            string result;

            if (CheckIfTransactionPossible(fromAccId, amount))
            {
                fromAcc.Balance = amount;
                toAcc.Balance = + amount;
                result = "Success";
            }
            else
            {
                result = "Lol you are too poor ";
            }
            return result;
        }

        public bool CheckIfTransactionPossible(int fromAccId, decimal amount)
        {
            var accounts = new List<Account>(); //todo get data
            var fromAcc = accounts.FirstOrDefault(x => x.AccountId == fromAccId);

            if (fromAcc.Balance <= amount && amount > 0)
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