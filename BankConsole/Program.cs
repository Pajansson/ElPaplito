using BankDatabaseRepo;
using System;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;
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
            Console.WriteLine("Press any key to continue...");
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
                int customerId;
                int.TryParse(Console.ReadLine(), out customerId);
                while (ValidateCustomerId(customerId, _repo) == false)
                {
                    Console.WriteLine("Could not find any customer, try again...");
                    Console.WriteLine("Press any key to try again");
                    int.TryParse(Console.ReadLine(), out customerId);
                }
                var result = ShowCustomer(_repo, customerId);
                Console.WriteLine("CustomerId: " + result.CustomerId);
                Console.WriteLine("Orginisation: " + result.OrginisationNumber);
                Console.WriteLine("Name: " + result.Name);
                Console.WriteLine("Adress: " + result.Adress + " Zipcode: " + result.ZipCode + " City: " + result.City + " Country: " + result.Country);
                Console.WriteLine();
                var accs = bankLogic.GetCustomersAccounts(customerId, _repo.AllAccounts());
                if (accs.Count == 0)
                {
                    Console.Write("This customer does not have any accounts.");
                }
                else
                {
                    decimal total = 0;
                    foreach (var acc in accs)
                    {
                        total += acc.Balance;
                        Console.Write("Account: " + acc.AccountId);
                        Console.WriteLine(" Balance: " + acc.Balance);
                    }
                    Console.WriteLine("Total balance: " + total);
                }
                ClearAndReoload(_repo, bankLogic);
            }
            else if (userChoice == "2")
            {
                int fromAccId = 0;
                decimal amount = 0;
                int fromCusId = 0;
                Console.WriteLine();
                Console.WriteLine("CustomerId: ");
                int.TryParse(Console.ReadLine(), out fromCusId);
                while (_repo.AllCustomers().FirstOrDefault(x => x.CustomerId == fromCusId) == null)
                {
                    Console.WriteLine("Could not find any customer, try again...");
                    int.TryParse(Console.ReadLine(), out fromCusId);
                }
                var cusAccs = bankLogic.GetCustomersAccounts(fromCusId, _repo.AllAccounts());
                foreach (var acc in cusAccs)
                {
                    Console.Write("Account: " + acc.AccountId);
                    Console.WriteLine(" Balance: " + acc.Balance);
                }
                Console.WriteLine("Which account?");
                int.TryParse(Console.ReadLine(), out fromAccId);
                var fromAcc = _repo.AllAccounts().FirstOrDefault(x => x.AccountId == fromAccId);
                while (_repo.AllAccounts().FirstOrDefault(x => x.AccountId == fromAccId) == null)
                {
                    Console.WriteLine("Could not find account, try again...");
                    int.TryParse(Console.ReadLine(), out fromAccId);
                }
                Console.WriteLine("Amount:");
                decimal.TryParse(Console.ReadLine(), out amount);
                if (amount > 0 || amount > fromAcc.Balance)
                {
                    Console.WriteLine("Withdraw failed!");
                }
                else
                {
                    bankLogic.Withdraw(amount, fromAccId, _repo);
                }

                Console.WriteLine("You now have " + fromAcc.Balance + "$ left!");
                Console.ReadKey();
                ClearAndReoload(_repo, bankLogic);
            }
            else if (userChoice == "3")
            {
                int fromCusId = 0;
                int fromAccId = 0;
                decimal amount = 0;
                Console.WriteLine();
                Console.WriteLine("CustomerId: ");
                int.TryParse(Console.ReadLine(), out fromCusId);
                while (_repo.AllCustomers().FirstOrDefault(x => x.CustomerId == fromCusId) == null)
                {
                    Console.WriteLine("Could not find any customer, try again...");
                    int.TryParse(Console.ReadLine(),out fromCusId);
                }
                var cusAccs = bankLogic.GetCustomersAccounts(fromCusId, _repo.AllAccounts());
                foreach (var acc in cusAccs)
                {
                    Console.Write("Account: " + acc.AccountId);
                    Console.WriteLine(" Balance: " + acc.Balance);
                }
                Console.WriteLine("Which account?");
                int.TryParse(Console.ReadLine(), out fromAccId);

                var fromAcc = _repo.AllAccounts().FirstOrDefault(x => x.AccountId == fromAccId);

                while (_repo.AllAccounts().FirstOrDefault(x => x.AccountId == fromAccId) == null)
                {
                    Console.WriteLine("Could not find account, try again...");
                     int.TryParse(Console.ReadLine(), out fromAccId);


                }
                Console.WriteLine("Amount:");
                decimal.TryParse(Console.ReadLine(), out amount);


                if (amount > 0)
                {
                    Console.WriteLine("No negative number are allowed");
                }
                else
                {
                    bankLogic.Deposit(amount, fromAccId, _repo);
                }

                Console.WriteLine("You now have " + fromAcc.Balance + "$ on your account!");
                Console.ReadKey();
                ClearAndReoload(_repo, bankLogic);



            }
            else if (userChoice == "4")
            {
                int fromAcc = 0;
                int toAcc = 0;

                Console.WriteLine();
                Console.WriteLine("Transaction");
                Console.WriteLine();
                Console.WriteLine("From Account: Enter Account id");
                Console.WriteLine();
                while (!int.TryParse(Console.ReadLine(), out fromAcc))
                {
                    Console.WriteLine("Please Enter a valid account id!");
                }
                Console.WriteLine();
                Console.WriteLine("To Account: Enter Account id");
                Console.WriteLine();
                while (!int.TryParse(Console.ReadLine(), out toAcc))
                {
                    Console.WriteLine("Please Enter a valid account id!");
                }
                Console.WriteLine();
                Console.WriteLine("Enter amount: ");
                Console.WriteLine();
                decimal amount = decimal.Parse(Console.ReadLine());
                var result = bankLogic.Transaction(_repo, fromAcc, toAcc, amount);
                if (result == "Success")
                {
                    Console.WriteLine($"Successful transaction. Transfered: {amount}$ from account: {fromAcc} to account: {toAcc}");
                }
                else
                {
                    Console.WriteLine("Failed transaction");
                }
                ClearAndReoload(_repo, bankLogic);
            }
            else if (userChoice == "5")
            {
                Console.WriteLine();
                Console.WriteLine("Exit and save");
                _repo.SaveDateToTextFile();
                Console.WriteLine("Info has been saved to: " + _repo.GetCurrentTextFile());
                Console.WriteLine();
                Console.WriteLine("Total Customers: " + _repo.AllCustomers().Count);
                Console.WriteLine("Total Accounts: " + _repo.AllAccounts().Count);
                Console.WriteLine("Total balance:" + _repo.AllAccounts().Sum(x => x.Balance));
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
                while (String.IsNullOrEmpty(adress))
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
                    ClearAndReoload(_repo, bankLogic);
                }
                else
                {
                    Console.WriteLine("Shit happens try again");
                    Console.WriteLine();
                    ClearAndReoload(_repo, bankLogic);
                }
            }
            else if (userChoice == "7")
            {
                Console.WriteLine();
                Console.WriteLine("Delete customer");
                Console.WriteLine();
                Console.WriteLine("Enter CustomerId: ");
                int customerId;
                int.TryParse(Console.ReadLine(), out customerId);
                while (ValidateCustomerId(customerId, _repo) == false)
                {
                    Console.WriteLine("Could not find any customer, try again...");
                    Console.WriteLine("Press any key to try again");
                    int.TryParse(Console.ReadLine(), out customerId);
                }
                if (bankLogic.ValidateDeleteCustomer(customerId, _repo.AllAccounts()) == false)
                {
                    Console.WriteLine("Can´t delete customer, customer still have money left!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    _repo.DeleteCustomer(customerId);
                    Console.WriteLine("Customer deleted, dount forget to save");
                    Console.WriteLine();
                }
                ClearAndReoload(_repo, bankLogic);
            }
            else if (userChoice == "8")
            {
                Console.WriteLine();
                Console.WriteLine("Create account");
                Console.WriteLine();
                Console.WriteLine("Enter CustomerId: ");
                int customerId;
                int.TryParse(Console.ReadLine(), out customerId);
                while (ValidateCustomerId(customerId, _repo) == false)
                {
                    Console.WriteLine("Could not find any customer, try again...");
                    Console.WriteLine("Press any key to try again");
                    int.TryParse(Console.ReadLine(), out customerId);
                }
                _repo.CreateAccount(customerId);
                Console.WriteLine("Your account has been created");
                ClearAndReoload(_repo, bankLogic);
            }
            else if (userChoice == "9")
            {
                Console.WriteLine();
                Console.WriteLine("Delete account");
                Console.WriteLine();
                Console.WriteLine("Enter Account id: ");
                int accountId;
                int.TryParse(Console.ReadLine(), out accountId);
                while (ValidateAccountId(accountId, _repo) == false)
                {
                    Console.WriteLine("ERROR! Invalid accountId or account still have money");
                    DisplayMenu(_repo, bankLogic);
                    int.TryParse(Console.ReadLine(), out accountId);
                }
                _repo.DeleteAccount(accountId);
                Console.WriteLine("Account deleted");
                ClearAndReoload(_repo, bankLogic);

            }
            Console.ReadLine();
        }

        private static bool ValidateAccountId(int accountId, DatabaseRepo repo)
        {
            var account = repo.AllAccounts().Find(x => x.AccountId == accountId);
            return account != null && account.Balance == 0;
        }

        private static bool ValidateCustomerId(int customerId, DatabaseRepo repo)
        {
            while (ShowCustomer(repo, customerId) == null)
            {
                return false;
            }
            return true;
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
                ClearAndReoload(_repo, bankLogic);
            }

        }

        private static Customer ShowCustomer(DatabaseRepo _repo, int customerId)
        {
            return _repo.AllCustomers().FirstOrDefault(x => x.CustomerId == customerId);
        }

        private static void ClearAndReoload(DatabaseRepo _repo, BankLogic bankLogic)
        {
            DrawStarLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
            Console.Beep();
            DisplayMenu(_repo, bankLogic);
        }

    }
}
