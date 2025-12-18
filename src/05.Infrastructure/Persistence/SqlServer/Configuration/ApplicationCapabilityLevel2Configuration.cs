using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Domain.Entities;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.Common.Constants;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.Common.Extensions;
using Pertamina.SolutionTemplate.Shared.Data.Constants;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer.Configuration;

public class ApplicationCapabilityLevel2Configuration : IEntityTypeConfiguration<ApplicationCapabilityLevel2>
{
    public void Configure(EntityTypeBuilder<ApplicationCapabilityLevel2> builder)
    {
        builder.ToTable(nameof(ISolutionTemplateDbContext.ApplicationCapabilityLevel2), nameof(SolutionTemplate));
        builder.ConfigureCreatableProperties();
        builder.ConfigureModifiableProperties();

        builder.Property(e => e.Nama).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.Name));
    }
}
