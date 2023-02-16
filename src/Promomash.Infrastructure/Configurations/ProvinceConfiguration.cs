using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Promomash.Core.Entities;

namespace Promomash.Infrastructure.Configurations;
public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();
    }
}