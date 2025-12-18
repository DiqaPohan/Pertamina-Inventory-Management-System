using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Domain.Entities;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.Common.Constants;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.Common.Extensions;
using Pertamina.SolutionTemplate.Shared.Data.Constants;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer.Configuration;

public class ApplicationCapabilityLevel1Configuration : IEntityTypeConfiguration<ApplicationCapabilityLevel1>
{
    public void Configure(EntityTypeBuilder<ApplicationCapabilityLevel1> builder)
    {
        builder.ToTable(nameof(ISolutionTemplateDbContext.ApplicationCapabilityLevel1), nameof(SolutionTemplate));
        builder.ConfigureCreatableProperties();
        builder.ConfigureModifiableProperties();

        builder.Property(e => e.Nama).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.Name));
    }
}
