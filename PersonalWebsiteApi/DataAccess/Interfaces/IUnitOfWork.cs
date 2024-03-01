using Core.DataKit;
using Core.DataKit.Result;

namespace PersonalWebsiteApi.DataAccess.Interfaces;

public interface IUnitOfWork
{
    IOwnerRepository UserRespository { get; set; }
    IArticleRepository ArticleRepository { get; set; }
    ICategoryRepository CategoriesRepository { get; set; }
    Task<Result<OK>> SaveAsync();
}
