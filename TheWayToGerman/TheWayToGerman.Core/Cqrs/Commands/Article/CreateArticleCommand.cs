using Core.Cqrs.Requests;
using Core.DataKit;
using System.Text.Json.Serialization;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Core.Cqrs.Responses;
using TheWayToGerman.Core.Entities;

namespace TheWayToGerman.Core.Cqrs.Commands.Article;

public class CreateArticleCommand : ICommand<CreateArticleCommandResponse>
{
    public string Title { get; set; }
    public string Overview { get; set; }
    public string Content { get; set; }
    [JsonIgnore]
    public Guid AutherID { get; set; }  
    public Guid CategoryID { get; set; }

}
