using BenchmarkDotNet.Running;
using System;
using System.Reflection;

namespace Npgsql.Benchmarks
{
    class Program
    {
       
        //static void Main(string[] args) => new BenchmarkSwitcher(typeof(Program).GetTypeInfo().Assembly).Run(args);

       //NpgsqlConnection con=new NpgsqlConnection("Server=192.168.1.48;User ID=opengauss;Password=openg23;Database=opengauss");

        static void Main()
        {
           
            var Server = new object();
            var UserID = new object();
            var password = new object();
            var Database = new object();
            Console.WriteLine("Server : ");
            Server = Console.ReadLine();
            Console.WriteLine("User ID : ");
            UserID = Console.ReadLine();
            Console.WriteLine("Password : ");        

            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;



                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);        



            password = Console.ReadLine();
            Console.WriteLine("Database : ");
            Database = Console.ReadLine();
            var DefaultConnectionString1 = "Server=" + Server + "; User ID=" + UserID + ";" + "Password=" + pass + ";" + "Database=" + Database;
            Console.WriteLine(DefaultConnectionString1);
            BenchmarkEnvironment.OpenConnectionAgain(DefaultConnectionString1);
            Console.WriteLine("\n");
            Console.WriteLine("Authentication Successful...");
          
        }      
     
        
    }
}
