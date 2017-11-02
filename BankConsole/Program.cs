﻿using BankDatabaseRepo;
using System;
using System.Linq;
using System.Threading;
using BankLib;

namespace BankConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var _repo = new DatabaseRepo();
            _repo.ImportAllData();
            DisplayMenu(_repo);
        }

        public static void DisplayMenu(DatabaseRepo _repo)
        {
            DrawStarLine();
            Console.WriteLine("* Welcome to ElPaplito, the worst bank in the world! *");
            DrawStarLine();
            Console.WriteLine("Importing from " + _repo.GetCurrentTextFile());
            Console.WriteLine("Total Customers: " + _repo.AllCustomers().Count());
            Console.WriteLine("Total Accounts: " + _repo.AllAccounts().Count());
            Console.WriteLine("Total balance: " + _repo.AllAccounts().Sum(x => x.Balance));
            Console.ReadLine();
            Console.WriteLine("0. Search");
            Console.WriteLine("1. Show customer");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Transaction");
            Console.WriteLine("5. Exit and save");
            Console.WriteLine("6. Create customer");
            Console.WriteLine("7. Delete customer");
            Console.WriteLine("8. Create account");
            Console.WriteLine("9. Delete account");
            Console.Write("Enter your choice: ");
            var userChoice = Console.ReadLine();

            if (userChoice == "0")
            {
                Console.WriteLine();
                Console.WriteLine("Search:");
                var result = Console.ReadLine();
                Search(_repo, result);


            }
            else if (userChoice == "1")
            {
                Console.WriteLine();
                Console.WriteLine("Insert customerId:");
                var customerId = Int32.Parse(Console.ReadLine());
                var result = ShowCustomer(_repo, customerId);
                Console.WriteLine("CustomerId: " + result.CustomerId);
                Console.WriteLine("Orginisation: " + result.OrginisationNumber);
                Console.WriteLine("Name: " + result.Name);
                Console.WriteLine("Adress: " + result.Adress);
            }
            else if (userChoice == "2")
            {
                Console.WriteLine();
                Console.WriteLine("From which account?");
                var fromAcc = Int32.Parse(Console.ReadLine());
                Console.WriteLine("To which account?");
                var toAcc = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Amount:");
                var amount = decimal.Parse(Console.ReadLine());
                
            }
            else if (userChoice == "3")
            {
                Console.WriteLine();
                Console.WriteLine("Deposit");
            }
            else if (userChoice == "4")
            {
                Console.WriteLine();
                Console.WriteLine("Transaction");
            }
            else if (userChoice == "5")
            {
                Console.WriteLine();
                Console.WriteLine("Exit and save");
                _repo.SaveDateToTextFile();
                Console.WriteLine("Info has been saved to: " + _repo.GetCurrentTextFile());
                Thread.Sleep(5000);
                Environment.Exit(0);


            }
            else if (userChoice == "6")
            {
                Console.WriteLine();
                Console.WriteLine("Create customer");
            }
            else if (userChoice == "7")
            {
                Console.WriteLine();
                Console.WriteLine("Delete customer");
            }
            else if (userChoice == "8")
            {
                Console.WriteLine();
                Console.WriteLine("Create account");
            }
            else if (userChoice == "9")
            {
                Console.WriteLine();
                Console.WriteLine("Delete account");
            }

            else
            {
                Console.WriteLine("Not a valid option!");
            }
            Console.ReadLine();
        }

        private static void DrawStarLine()
        {
            Console.WriteLine("*****************************************************");

        }

        private static void Search(DatabaseRepo _repo, string searchPatern)
        {
            var result = _repo.AllCustomers().Where(x => x.Name.Contains(searchPatern) || x.City.Contains(searchPatern)).ToList();

            if (result.Count() == 0)
            {
                Console.WriteLine($"Nein nobody here");
            }
            else
            {
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.CustomerId} : {item.Name}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Search again press 0 else press 1");
            var input = Console.ReadLine();

            if (input == "0")
            {
                Console.WriteLine();
                DrawStarLine();
                Console.WriteLine();
                Console.WriteLine("Search");
                var result1 = Console.ReadLine();
                Search(_repo, result1);
            }
            else
            {
                DisplayMenu(_repo);
            }

        }

        private static Customer ShowCustomer(DatabaseRepo _repo, int customerId)
        {
            return _repo.AllCustomers().FirstOrDefault(x => x.CustomerId == customerId);
        }
    }
}
