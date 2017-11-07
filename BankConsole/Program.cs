using BankDatabaseRepo;
using System;
using System.Linq;
using System.Threading;
using BankLib;
using BankLogicRepo;

namespace BankConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var _repo = new DatabaseRepo();
            var banklogic = new BankLogic();
            _repo.ImportAllData();
            DisplayMenu(_repo, banklogic);
        }

        public static void DisplayMenu(DatabaseRepo _repo, BankLogic bankLogic)
        {
            DrawStarLine();
            Console.WriteLine("* Welcome to ElPaplito, the worst bank in the world! *");
            DrawStarLine();
            Console.WriteLine("Importing from " + _repo.GetCurrentTextFile());
            Console.WriteLine("Total Customers: " + _repo.AllCustomers().Count);
            Console.WriteLine("Total Accounts: " + _repo.AllAccounts().Count);
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
                Search(_repo, bankLogic, result.ToLower());

            }
            else if (userChoice == "1")
            {
                Console.WriteLine();
                Console.WriteLine("CustomerId:");
                var customerId = Int32.Parse(Console.ReadLine());
                while (ShowCustomer(_repo, customerId) == null)
                {
                    Console.WriteLine("Could not find any customer, try again...");
                    customerId = Int32.Parse(Console.ReadLine());
                }
                var result = ShowCustomer(_repo, customerId);
                Console.WriteLine("CustomerId: " + result.CustomerId);
                Console.WriteLine("Orginisation: " + result.OrginisationNumber);
                Console.WriteLine("Name: " + result.Name);
                Console.WriteLine("Adress: " + result.Adress);
                var accs = bankLogic.GetCustomersAccounts(customerId, _repo.AllAccounts());
                foreach (var acc in accs)
                {
                    Console.Write("Account: " + acc.AccountId);
                    Console.WriteLine(" Balance: " + acc.Balance);
                }
                Console.ReadKey();
                DisplayMenu(_repo, bankLogic);
            }
            else if (userChoice == "2")
            {
                Console.WriteLine();
                Console.WriteLine("CustomerId: ");
                var fromCusId = Int32.Parse(Console.ReadLine());
                while (_repo.AllCustomers().FirstOrDefault(x => x.CustomerId == fromCusId) == null)
                {
                    Console.WriteLine("Could not find any customer, try again...");
                    fromCusId = Int32.Parse(Console.ReadLine());
                }
                var cusAccs = bankLogic.GetCustomersAccounts(fromCusId, _repo.AllAccounts());
                foreach (var acc in cusAccs)
                {
                    Console.Write("Account: " + acc.AccountId);
                    Console.WriteLine(" Balance: " + acc.Balance);
                }
                Console.WriteLine("Which account?");
                var fromAccId = Int32.Parse(Console.ReadLine());
                var fromAcc = _repo.AllAccounts().FirstOrDefault(x => x.AccountId == fromAccId);
                while (_repo.AllAccounts().FirstOrDefault(x => x.AccountId == fromAccId) == null)
                {
                    Console.WriteLine("Could not find account, try again...");
                    fromAccId = Int32.Parse(Console.ReadLine());
                }
                Console.WriteLine("Amount:");
                var amount = (Console.ReadLine());
                if (amount.Contains("-") || decimal.Parse(amount) > fromAcc.Balance)
                {
                    Console.WriteLine("Withdraw failed!");
                }
                else
                {
                    bankLogic.Withdraw(decimal.Parse(amount), fromAccId, _repo);
                }

                Console.WriteLine("You now have " + fromAcc.Balance + "$ left!");
                Console.ReadKey();
                DisplayMenu(_repo, bankLogic);
            }
            else if (userChoice == "3")
            {
                Console.WriteLine();
                Console.WriteLine("Deposit");
                Console.WriteLine("Enter account id:");
                var id = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Amount");
                var amount = Console.ReadLine();

                if (amount.Contains("-"))
                {
                    Console.WriteLine("No negative number are allowed");
                }
                else
                {
                    bankLogic.Deposit(decimal.Parse(amount), id, _repo);
                }

                DisplayMenu(_repo, bankLogic);



            }
            else if (userChoice == "4")
            {
                Console.WriteLine();
                Console.WriteLine("Transaction");
                Console.WriteLine();
                Console.WriteLine("From Account: Enter Account id");
                Console.WriteLine();
                int fromAccId = Int32.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("To Account: Enter Account id");
                Console.WriteLine();
                int toAccId = Int32.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Enter amount: ");
                Console.WriteLine();
                decimal amount = Int32.Parse(Console.ReadLine());
                var result = bankLogic.Transaction(_repo, fromAccId, toAccId, amount);
                if (result == "Success")
                {
                    Console.WriteLine("Successful transaction.");
                }
                else
                {
                    Console.WriteLine("Failed transaction");
                }
                Console.ReadKey();
                DisplayMenu(_repo, bankLogic);
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
                int orgNo = 0;

                Console.WriteLine();
                Console.WriteLine("Create customer:");
                Console.WriteLine();

                Console.WriteLine("Name:");
                var name = Console.ReadLine();
                while (String.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Please enter a valid Name");
                    name = Console.ReadLine();
                }

                Console.WriteLine("Organisation number:");
                while (!int.TryParse(Console.ReadLine(), out orgNo))
                {
                    Console.WriteLine("Please Enter a valid numerical value!");
                    Console.WriteLine("Please Enter an ID number:");
                }

                Console.WriteLine("Adress:");
                var adress = Console.ReadLine();
                while (String.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Please enter a valid Adress");
                    adress = Console.ReadLine();
                }

                Console.WriteLine("City:");
                var city = Console.ReadLine();
                while (String.IsNullOrEmpty(city))
                {
                    Console.WriteLine("Please enter a valid city");
                    city = Console.ReadLine();
                }

                Console.WriteLine("State:");
                var state = Console.ReadLine();

                Console.WriteLine("ZipCode:");
                var zipcode = Console.ReadLine();
                while (String.IsNullOrEmpty(zipcode))
                {
                    Console.WriteLine("Please enter a valid zipcode");
                    zipcode = Console.ReadLine();
                }

                Console.WriteLine("Country:");
                var country = Console.ReadLine();
                while (String.IsNullOrEmpty(country))
                {
                    Console.WriteLine("Please enter a valid country");
                    country = Console.ReadLine();
                }

                Console.WriteLine("Phone:");
                var phone = Console.ReadLine();
                while (String.IsNullOrEmpty(phone))
                {
                    Console.WriteLine("Please enter a valid phone");
                    phone = Console.ReadLine();
                }

                var result = _repo.CreateCustomer(name, adress, phone, city, country, zipcode, orgNo.ToString(), state);
                if (result)
                {
                    DrawStarLine();
                    Console.WriteLine("Customer added dount forget to save!");
                    Console.Beep();
                    DrawStarLine();
                    Console.WriteLine(); Console.WriteLine();
                    DisplayMenu(_repo, bankLogic);
                }
                else
                {
                    Console.WriteLine("Shit happens try again");
                    Console.WriteLine();
                    DisplayMenu(_repo, bankLogic);
                }
            }
            else if (userChoice == "7")
            {
                Console.WriteLine();
                Console.WriteLine("Delete customer");
                Console.WriteLine();
                Console.WriteLine("Enter customerId");

                var id = Console.ReadLine();
                if (id != "")
                {
                    var result = _repo.DeleteCustomer(Int32.Parse(id));
                    if (result)
                    {
                        Console.WriteLine("Customer deleted, dount forget to save");
                        Console.WriteLine();
                        DisplayMenu(_repo, bankLogic);
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong. Wrong id?");
                        Console.WriteLine();
                        DisplayMenu(_repo, bankLogic);
                    }
                }



            }
            else if (userChoice == "8")
            {
                Console.WriteLine();
                Console.WriteLine("Create account");
                Console.WriteLine();
                Console.WriteLine("Enter Customer id: ");
                int id = Int32.Parse(Console.ReadLine());
                _repo.CreateAccount(id);

            }
            else if (userChoice == "9")
            {
                Console.WriteLine();
                Console.WriteLine("Delete account");
                Console.WriteLine();
                Console.WriteLine("Enter Account id: ");
                int id = Int32.Parse(Console.ReadLine());
                _repo.DeleteAccount(id);
            }

            else
            {
                Console.WriteLine("Not a valid option!");
            }
            Console.ReadLine();
        }

        private static void DrawStarLine()
        {
            Console.WriteLine("******************************************************");

        }

        private static void Search(DatabaseRepo _repo, BankLogic bankLogic, string searchPatern)
        {
            var result = _repo.AllCustomers().Where(x => x.Name.ToLower().Contains(searchPatern) || x.City.ToLower().Contains(searchPatern)).ToList();

            if (!result.Any())
            {
                Console.WriteLine("Nein nobody here");
            }
            else
            {
                
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.CustomerId} : {item.Name}");
                }
                
            }
            DrawStarLine();
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
                Search(_repo, bankLogic, result1);
            }
            else
            {
                Console.Beep();
                Console.Clear();
                DisplayMenu(_repo, bankLogic);
            }

        }

        private static Customer ShowCustomer(DatabaseRepo _repo, int customerId)
        {
            return _repo.AllCustomers().FirstOrDefault(x => x.CustomerId == customerId);
        }

    }
}
