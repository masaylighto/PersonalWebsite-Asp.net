
using Core.Cqrs.Requests;
using Core.DataKit;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Core.Cqrs.Responses;
using TheWayToGerman.Core.Enums;
using TheWayToGerman.Core.Helpers;

namespace TheWayToGerman.Api.DTO.Category;

public class CreateCategoryDTO 
{
    public required string Name { get; set; }
    public Guid LanguageID { get; set; } = new Guid(DefaultDBValues.DefaultLanguageID);//For now, we need only one language,
                                                                                       //So we use its ID,
                                                                                       //But later we might need more than one language that why we specify it in the DTO to be chosen by the user.
}
