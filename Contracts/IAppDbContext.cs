using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Contracts
{
    public interface IAppDbContext
    {
        DbSet<T> Set<T>() where T : class;
        EntityEntry<T> Entry<T>(T entry) where T : class;
        DbSet<Flight> Flights { get; set; }
        DbSet<Airport> Airports { get; set; }

        int SaveChanges();
    }
}
