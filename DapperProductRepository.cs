using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMandDapperExercise
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public IEnumerable<Products> GetAllProducts()
        {
            var pro = _connection.Query<Products>("SELECT * FROM PRODUCTS");
            return pro;
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID) VALUES (@name, @price, @categoryID);",
           new { name = name, price = price, categoryID = categoryID });
        }
        public void UpdateProduct(string updateName, double updatePrice)
        {
            _connection.Execute("UPDATE products SET Price = @updatePrice WHERE Name = @updateName;",
            new { updateName = updateName, updatePrice = updatePrice });
        }
        public void DeleteProduct(string deleteName)
        {
            _connection.Execute("DELETE FROM products WHERE Name = @deleteName;",
               new { deleteName = deleteName });
        }
    }
}

