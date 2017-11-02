using BankLib;
using System;
using System.Collections.Generic;
using System.Linq;
using BankDatabaseRepo;

namespace BankLogicRepo
{
    public class BankLogic
    {
        public string Transaction(int fromAccId, int toAccId, decimal amount)
        {
            var bankDb = new DatabaseRepo();
            var accounts = bankDb.AllAccounts();
            var fromAcc = accounts.FirstOrDefault(x => x.AccountId == fromAccId);
            var toAcc = accounts.FirstOrDefault(x => x.AccountId == toAccId);
            string result;

            if (CheckIfTransactionPossible(fromAcc, amount))
            {
                fromAcc.Balance -= amount;
                toAcc.Balance += amount;
                bankDb.CreateTransaction(new Transaction
                {
                    Amount = amount,
                    FromAccountId = fromAccId,
                    ToAccountId = toAccId
                });
                result = "Success";
            }
            else
            {
                result = "Lol you are too poor ";
            }
            return result;
        }

        public bool CheckIfTransactionPossible(Account fromAcc, decimal amount)
        {
            if (fromAcc.Balance <= amount && amount > 0)
            {
                return false;
            }
            return true;
        }

        public void Deposit(decimal amount, int accountId, DatabaseRepo _Repo)
        {
            var account = _Repo.AllAccounts().FirstOrDefault(x => x.AccountId == accountId);

            if (amount > 0)
            {
                account.Balance += amount;
            }
        }

        public void Withdraw(decimal amount, Account account)
        {
            if (amount > 0 && account.Balance >= amount)
            {
                account.Balance = -amount;
            }
        }

        public List<Account> GetCustomersAccounts(int customerId, List<Account> accounts)
        {
            return accounts.Where(x => x.CustomerId == customerId).ToList();
        }

    }
}