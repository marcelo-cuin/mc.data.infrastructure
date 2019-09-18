using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace connector.infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        string ConnectionStringName { get; }
        DbContext Context { get; }
        Task Commit();
    }
}
