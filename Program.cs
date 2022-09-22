using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Drawing.Printing;
using Org.BouncyCastle.Asn1.IsisMtt.X509;

namespace ORMandDapperExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion

            IDbConnection conn = new MySqlConnection(connString);
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Hello user, Here are the current departments:");
            var depos = repo.GetAllDepartments();
            Print(repo.GetAllDepartments());
            Console.WriteLine("--------------------------");

            Console.WriteLine("Do you want to add a department? Yes or No");
            string userResponse = Console.ReadLine();
            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of the department?");
                userResponse = Console.ReadLine();
                repo.InsertDepartment(userResponse);
                Print(repo.GetAllDepartments());
            }
            Console.WriteLine("--------------------------");

            //Exercise 2
            DapperProductRepository pro = new DapperProductRepository(conn);
            Console.WriteLine("Hello user, Here are the current products:");
            var pros = pro.GetAllProducts();
            Console.WriteLine("--------------------------");
            Print(pro.GetAllProducts());
            Console.WriteLine("--------------------------");

            Console.WriteLine("Do you want to add a product? Yes or No");
            string userResponse2 = Console.ReadLine();
            if (userResponse2.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of the product?");
                string userResponse3 = Console.ReadLine();
                Console.WriteLine("What is the price of the product?");
                double userResponse4 = double.Parse(Console.ReadLine());
                Console.WriteLine("What is the Category ID of the product?");
                int userResponse5 = int.Parse(Console.ReadLine());
                pro.CreateProduct(userResponse3, userResponse4, userResponse5);
                Print(pro.GetAllProducts());
            }
            //Bonus 1
            Console.WriteLine("--------------------------");
            Console.WriteLine("Would you like to update a product? Yes or No");
            string userResponse8 = Console.ReadLine();
            if (userResponse8.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of the product?");
                string userResponse9 = Console.ReadLine();
                Console.WriteLine("What is the price of the product?");
                double userResponse10 = double.Parse(Console.ReadLine());
                pro.UpdateProduct(userResponse9, userResponse10);
                Print(pro.GetAllProducts());
            }
            
            //Bonus 2
            Console.WriteLine("--------------------------");
            Console.WriteLine("Would you like to delete a product? Yes or No");
            string userResponse6 = Console.ReadLine();
            if (userResponse6.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of the product?");
                string userResponse7 = Console.ReadLine().ToLower();
                pro.DeleteProduct(userResponse7);
                Print(pro.GetAllProducts());
            }
            
        }
        private static void Print(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"Department ID: {depo.DepartmentID} Department Name: {depo.Name}");
            }
        }
        private static void Print(IEnumerable<Products> pros)
        {
            foreach (var pro in pros)
            {
                Console.WriteLine($"Product Name: {pro.Name} Product Price: {pro.Price} Product ID: {pro.CategoryID}");
            }
        }
    }
}