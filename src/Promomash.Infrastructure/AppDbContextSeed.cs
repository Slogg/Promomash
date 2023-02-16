using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Promomash.Core.Entities;

namespace Promomash.Infrastructure;

public class AppDbContextSeed
{
    public static async Task SeedAsync(AppDbContext catalogContext,
        ILogger logger,
        int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            if (catalogContext.Database.IsSqlServer())
            {
                catalogContext.Database.Migrate();
            }

            if (!await catalogContext.Countries.AnyAsync())
            {
                await catalogContext.Countries.AddRangeAsync(
                    GetCountries());

                await catalogContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;

            logger.LogError(ex.Message);
            await SeedAsync(catalogContext, logger, retryForAvailability);
            throw;
        }
    }


    static IEnumerable<Country> GetCountries()
    {
        return new List<Country>
        {
            new Country(1, "Country 1", new List<Province>
            {
                new Province(1, "Province 1.1"),
                new Province(2, "Province 1.2")
            }),
            new Country(2, "Country 2", new List<Province>
            {
                new Province(3, "Province 2.1"),
                new Province(4, "Province 2.2")
            })
        };
    }

}
