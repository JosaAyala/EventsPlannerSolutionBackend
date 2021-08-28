using EventsPlanner.DomainContext.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.Services
{
    public interface IUnitOfWork : IDisposable
    {
        //DomainDataContext Context { get; }
        void Commit();
    }
}
