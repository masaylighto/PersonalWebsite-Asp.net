using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using Core.Expressions;
using Mapster;
using System.Linq.Expressions;
using TheWayToGerman.Core.Cqrs.Queries.Article;
using TheWayToGerman.Core.Cqrs.Queries.Category;
using TheWayToGerman.Core.Cqrs.Responses;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.DataAccess.Interfaces;

namespace TheWayToGerman.Logic.Cqrs.Queries;

public class GetArticleQueryHandler : QueryHandlerAsync<GetArticleQuery,GetArticleQueryResponse>
{
    public IUnitOfWork UnitOfWork { get; }

    public GetArticleQueryHandler(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    protected override async Task<Result<GetArticleQueryResponse>> Fetch(GetArticleQuery request, CancellationToken cancellationToken)
    {
       return (await UnitOfWork.ArticleRepository.GetAsync(request.ID)).Adapt<GetArticleQueryResponse>();
    }
}
