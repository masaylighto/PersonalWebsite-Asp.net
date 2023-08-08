

using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Entities;

public class LocalizedBaseEntity : BaseEntity
{
    public required Language Language { get; set; }
}
