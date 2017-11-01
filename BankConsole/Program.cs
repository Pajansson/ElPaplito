using BankDatabaseRepo;
using System;

namespace BankConsole
{
    class Program
    {
        static void Main(string[] args)
        {
          DisplayMenu();

            var dbRepo = new DatabaseRepo();


            var x = dbRepo.ImportAllData();
            dbRepo.SaveDateToTextFile();
        }

        public static void DisplayMenu()
        {
            DrawStarLine();
            Console.WriteLine("* Welcome to ElPaplito, the best bank in the world! *");
            DrawStarLine();
            Console.WriteLine("Importing");
            Console.WriteLine("Total Customers:" );
            Console.WriteLine("Total Accounts:");
            Console.WriteLine("Total balance:");
            Console.ReadLine();
            Console.WriteLine("0. Show customer");
            Console.WriteLine("1. Search"); 
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
                Console.WriteLine("Transaction");
            else if (userChoice == "1")
            {
                Console.WriteLine("Withdraw");
            }
            else if (userChoice == "2")
            {
                Console.WriteLine("Withdraw");
            }
            else if (userChoice == "3")
            {
                Console.WriteLine("Deposit");
            }
            else if (userChoice == "4")
            {
                Console.WriteLine("Search");
            }
            else if (userChoice == "5")
            {
                Console.WriteLine("Search");
            }
            else if (userChoice == "6")
            {
                Console.WriteLine("Search");
            }
            else if (userChoice == "7")
            {
                Console.WriteLine("Search");
            }
            else if (userChoice == "8")
            {
                Console.WriteLine("Search");
            }
            else if (userChoice == "9")
            {
                Console.WriteLine("Search");
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

    }
}
