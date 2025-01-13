using System;
using System.Data.SqlClient;
using TechPay.Core.Interfaces;
using TechPay.Infrastructure.Repositories;

namespace TechPay.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;

        public ICustomerRepository Customers { get; }

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            Customers = new CustomerRepository(_connection, _transaction);
        }

        public void Commit()
        {
            _transaction.Commit();
            _transaction = _connection.BeginTransaction(); // Start a new transaction
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}

