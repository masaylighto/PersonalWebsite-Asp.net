using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using Core.Expressions;
using Mapster;
using System.Linq.Expressions;
using TheWayToGerman.Core.Cqrs.Queries.Article;
using TheWayToGerman.Core.Cqrs.Queries.Category;
using TheWayToGerman.Core.Cqrs.Responses;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Helpers;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.Logic.Cqrs.Queries;

public class GetArticleQueryHandler : QueryHandlerAsync<GetArticleQuery,GetArticleQueryResponse>
{
    public IUnitOfWork UnitOfWork { get; }
    public ArticlesHandler ArticlesHandler { get; }

    public GetArticleQueryHandler(IUnitOfWork unitOfWork, ArticlesHandler articlesHandler)
    {
        UnitOfWork = unitOfWork;
        ArticlesHandler = articlesHandler;
    }

    protected override async Task<Result<GetArticleQueryResponse>> Fetch(GetArticleQuery request, CancellationToken cancellationToken)
    {
        var articleResult = await UnitOfWork.ArticleRepository.GetAsync(request.ID);
        if (articleResult.ContainError())
        {
            return articleResult.GetError();
        }

        var article = articleResult.GetData();        
        var loadArticleContentResult = ArticlesHandler.LoadArticleContent(article.Content);
        if (loadArticleContentResult.IsNotOk())
        {
            return loadArticleContentResult.GetError();
        }

        var reMergeImageResult = await ArticlesHandler.ReMergeImage();
        if (reMergeImageResult.IsNotOk())
        {
            return reMergeImageResult.GetError();
        }

        var modifiedArticleResult = ArticlesHandler.GetArticleContent();
        if (modifiedArticleResult.ContainError())
        {
            return modifiedArticleResult.GetError();
        }

        var modifiedArticle = modifiedArticleResult.GetData();
        article.Content = modifiedArticle;
        return GetArticleQueryResponse.From(article);

    }
}
