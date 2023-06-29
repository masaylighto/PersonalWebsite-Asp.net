using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Helpers;

namespace TheWayToGerman.Core.Database;

public class LanguageTableConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasData(new Language()
        {
            CreateDate = DateTime.UtcNow,
            Id = new Guid(DefaultDBValues.DefaultLanguageID),
            LanguageName = "arabic",
            WritingDirection = Enums.LanguageWritingDirection.RightToLeft,
        });
    }
}
