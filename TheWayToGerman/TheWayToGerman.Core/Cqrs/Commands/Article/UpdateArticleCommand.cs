using Core.Cqrs.Requests;
using Core.DataKit;
using TheWayToGerman.Core.Cqrs.Queries;
using TheWayToGerman.Core.Entities;

namespace TheWayToGerman.Core.Cqrs.Commands.Article;

public class UpdateArticleCommand : ICommand<OK>
{
    public Guid ID { get; set; }
    public string Title { get; set; }
    public string Overview { get; set; }
    public string Content { get; set; }
    public Guid CategoryID { get; set; }

}
