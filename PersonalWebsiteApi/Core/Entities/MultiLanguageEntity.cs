

using PersonalWebsiteApi.Core.Enums;

namespace PersonalWebsiteApi.Core.Entities;

public class LocalizedBaseEntity : BaseEntity
{
    public required Language Language { get; set; }
}
