using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestAPI.Models;

namespace TestAPI.Database
{
  public interface IWeatherDatabase
  {
    DbSet<Summary> Summaries { get; set; }
    DbSet<Forecast> Forecasts { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    void Migrate();
  }
}