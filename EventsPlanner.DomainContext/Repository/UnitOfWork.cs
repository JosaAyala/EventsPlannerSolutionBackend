using EventsPlanner.DomainContext.DataContext;
using EventsPlanner.DomainContext.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.DomainContext.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        public DomainDataContext Context { get; }

        public UnitOfWork(DomainDataContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
