using BankDatabaseRepo;
using System;

namespace BankConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbRepo = new DatabaseRepo();

            dbRepo.SaveDateToTextFile();
            Console.WriteLine("Hello World!");
        }
    }
}
