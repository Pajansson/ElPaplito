using BankDatabaseRepo;
using System;

namespace BankConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbRepo = new DatabaseRepo();


            var x = dbRepo.ImportAllData();
            dbRepo.SaveDateToTextFile();
            foreach (var accounts in x.Item1)
            {
                Console.WriteLine("AccountId:{0}, CustomerId:{1}, Balance:{2}", accounts.AccountId, accounts.CustomerId, accounts.Balance);
            }

            foreach (var customer in x.Item2)
            {
                Console.WriteLine("CustomerId:{0}, OrgNr:{1}, Name:{2}, Address:{3}, City:{4}, State:{5}, Zipcode:{6}, Country:{7}, Phone:{8}",
                    customer.CustomerId, customer.OrginisationNumber, customer.Name, customer.Adress, customer.City, customer.State, customer.ZipCode, customer.Country, customer.Phone);
            }
            Console.ReadLine();
        }
    }
}
