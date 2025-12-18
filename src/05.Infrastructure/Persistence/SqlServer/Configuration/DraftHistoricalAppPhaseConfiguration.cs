//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Pertamina.SolutionTemplate.Application.Services.Persistence;
//using Pertamina.SolutionTemplate.Domain.Entities;
//using Pertamina.SolutionTemplate.Infrastructure.Persistence.Common.Constants;
//using Pertamina.SolutionTemplate.Infrastructure.Persistence.Common.Extensions;
//using Pertamina.SolutionTemplate.Shared.Data.Constants;

//namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer.Configuration;

//public class DraftHistoricalAppPhaseConfiguration : IEntityTypeConfiguration<DraftHistoricalAppPhase>
//{
//    public void Configure(EntityTypeBuilder<DraftHistoricalAppPhase> builder)
//    {
//        builder.ToTable(nameof(ISolutionTemplateDbContext.DraftHistoricalAppPhase), nameof(SolutionTemplate));
//        builder.ConfigureCreatableProperties();
//        builder.ConfigureModifiableProperties();

//        builder.Property(e => e.Code_Apps).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.Name));
//        //builder.Property(e => e.Description).HasColumnType(CommonColumnTypes.Nvarchar(MaximumLengthFor.Description));
//    }
//}
