using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mc.data.infrastructure
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }
        Task Commit();
    }
}
