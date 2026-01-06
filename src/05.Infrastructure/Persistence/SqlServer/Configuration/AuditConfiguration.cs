using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Services.Persistence;
using Pertamina.SolutionTemplate.Base.ValueObjects;
using Pertamina.SolutionTemplate.Domain.Entities;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.Common.Constants;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.Common.Extensions;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer.Constants;
using Pertamina.SolutionTemplate.Shared.Audits.Constants;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer.Configuration;

public class AuditConfiguration : IEntityTypeConfiguration<Audit>
{
    public void Configure(EntityTypeBuilder<Audit> builder)
    {
        builder.ToTable(nameof(ISolutionTemplateDbContext.Audits), nameof(SolutionTemplate));
        builder.ConfigureCreatableProperties();

        builder.Property(e => e.TableName).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.TableName));
        builder.Property(e => e.EntityName).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.EntityName));
        builder.Property(e => e.ActionType).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.ActionType));
        builder.Property(e => e.ActionName).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.ActionName));
        builder.Property(e => e.OldValues).HasColumnType(ColumnTypes.NvarcharMax);
        builder.Property(e => e.NewValues).HasColumnType(ColumnTypes.NvarcharMax);
        builder.Property(e => e.ClientApplicationId).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.ClientApplicationId));
        builder.Property(e => e.FromIpAddress).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.FromIpAddress));

        builder.OwnsOne(o => o.FromGeolocation, p =>
        {
            p.Property(p => p.Latitude).HasColumnName(nameof(Geolocation.Latitude));
            p.Property(p => p.Longitude).HasColumnName(nameof(Geolocation.Longitude));
            p.Property(p => p.Accuracy).HasColumnName(nameof(Geolocation.Accuracy));
        });
    }
}
