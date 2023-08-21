using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using Core.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TheWayToGerman.Core.Cqrs.Queries.Article;
using TheWayToGerman.Core.Cqrs.Queries.Category;
using TheWayToGerman.Core.Cqrs.Responses;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.Logic.Cqrs.Queries;

public class GetArticlesQueryHandler : QueryHandler<GetArticlesQuery, IEnumerable<GetArticlesQueryResponse>>
{
    public IUnitOfWork UnitOfWork { get; }

    public GetArticlesQueryHandler(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    protected override Result<IEnumerable<GetArticlesQueryResponse>> Fetch(GetArticlesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Article, bool>> predicate = (article) => true;
        if (request.Title is not null)
        {
            string serachPattern = $"%{request.Title.ToLower()}%";
            predicate.And(x=>EF.Functions.Like(x.Title.ToLower(), serachPattern));
        }
        if (request.Description is not null)
        {
            string serachPattern = $"%{request.Description.ToLower()}%";
            predicate.And(x => EF.Functions.Like(x.Content.ToLower(), serachPattern) || EF.Functions.Like(x.Overview.ToLower(), serachPattern));
        }
        return UnitOfWork.ArticleRepository.Get(predicate, x => x.Adapt<GetArticlesQueryResponse>());
    }
}
