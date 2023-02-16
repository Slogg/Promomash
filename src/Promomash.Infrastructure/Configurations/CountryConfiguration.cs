using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Promomash.Core.Entities;

namespace Promomash.Infrastructure.Configurations;
public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();
    }
}