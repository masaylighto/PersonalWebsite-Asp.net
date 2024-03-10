using Core.Cqrs.Requests;
using Core.DataKit;
using System.Text.Json.Serialization;
using PersonalWebsiteApi.Core.Cqrs.Queries;
using PersonalWebsiteApi.Core.Cqrs.Responses;
using PersonalWebsiteApi.Core.Entities;

namespace PersonalWebsiteApi.Core.Cqrs.Commands.Article;

public class CreateArticleCommand : ICommand<CreateArticleCommandResponse>
{
    public string Title { get; set; } 
    public string Overview { get; set; }
    public string Picture { get; set; } 
    public string Content { get; set; }
    public Guid CategoryID { get; set; } 
   

}
