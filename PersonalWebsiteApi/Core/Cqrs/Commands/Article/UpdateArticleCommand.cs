using Core.Cqrs.Requests;
using Core.DataKit;
using System.Text.Json.Serialization;
using PersonalWebsiteApi.Core.Cqrs.Queries;
using PersonalWebsiteApi.Core.Entities;

namespace PersonalWebsiteApi.Core.Cqrs.Commands.Article;

public class UpdateArticleCommand : ICommand<OK>
{
    public Guid ID { get; set; }   
    public string Title { get; set; }
    public string Overview { get; set; }
    public string Content { get; set; }
    public Guid CategoryID { get; set; }

}
