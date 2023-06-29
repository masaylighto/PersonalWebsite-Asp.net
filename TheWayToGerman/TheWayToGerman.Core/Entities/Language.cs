
using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Entities;

public class Language:BaseEntity
{
    public string LanguageName{ get; set; }
    public LanguageWritingDirection WritingDirection { get; set; }
}
