using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TechPay.Core.Entities;
using TechPay.Core.Interfaces;

namespace TechPay.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public CustomerRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();
            var command = new SqlCommand("SELECT * FROM Customers", _connection, _transaction);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    customers.Add(new Customer
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        AccountNumber = reader["AccountNumber"].ToString(),
                        Balance = Convert.ToDecimal(reader["Balance"])
                    });
                }
            }

            return customers;
        }

        public Customer GetCustomerById(int id)
        {
            var command = new SqlCommand("SELECT * FROM Customers WHERE Id = @Id", _connection, _transaction);
            command.Parameters.AddWithValue("@Id", id);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Customer
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        AccountNumber = reader["AccountNumber"].ToString(),
                        Balance = Convert.ToDecimal(reader["Balance"])
                    };
                }
            }

            return null;
        }

        public void AddCustomer(Customer customer)
        {
            var command = new SqlCommand("INSERT INTO Customers (Name, AccountNumber, Balance) VALUES (@Name, @AccountNumber, @Balance)", _connection, _transaction);
            command.Parameters.AddWithValue("@Name", customer.Name);
            command.Parameters.AddWithValue("@AccountNumber", customer.AccountNumber);
            command.Parameters.AddWithValue("@Balance", customer.Balance);
            command.ExecuteNonQuery();
        }

        public void UpdateCustomer(Customer customer)
        {
            var command = new SqlCommand("UPDATE Customers SET Name = @Name, Balance = @Balance WHERE Id = @Id", _connection, _transaction);
            command.Parameters.AddWithValue("@Name", customer.Name);
            command.Parameters.AddWithValue("@Balance", customer.Balance);
            command.Parameters.AddWithValue("@Id", customer.Id);
            command.ExecuteNonQuery();
        }

        public void DeleteCustomer(int id)
        {
            var command = new SqlCommand("DELETE FROM Customers WHERE Id = @Id", _connection, _transaction);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }
    }
}

