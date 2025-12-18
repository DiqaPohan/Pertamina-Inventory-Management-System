using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Domain.Entities;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.Common.Constants;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.Common.Extensions;
using Pertamina.SolutionTemplate.Shared.Data.Constants;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer.Configuration;

public class ApplicationCriticalityConfiguration : IEntityTypeConfiguration<ApplicationCriticality>
{
    public void Configure(EntityTypeBuilder<ApplicationCriticality> builder)
    {
        builder.ToTable(nameof(ISolutionTemplateDbContext.ApplicationCriticality), nameof(SolutionTemplate));
        builder.ConfigureCreatableProperties();
        builder.ConfigureModifiableProperties();

        builder.Property(e => e.Nama).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.Name));
    }
}
