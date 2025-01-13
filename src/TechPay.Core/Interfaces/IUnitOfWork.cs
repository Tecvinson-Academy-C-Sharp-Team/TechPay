using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechPay.Infrastructure;

namespace TechPay.Core.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        void Commit();
        void Dispose();
    }
}

