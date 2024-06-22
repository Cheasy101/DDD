using DDD.APP.Domain_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.APP.Infrastructure_Layer.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.FullName).IsRequired().HasMaxLength(100);
        builder.HasIndex(e => e.FullName).IsUnique();
        builder.Property(e => e.Position).IsRequired().HasMaxLength(50);
    }
}