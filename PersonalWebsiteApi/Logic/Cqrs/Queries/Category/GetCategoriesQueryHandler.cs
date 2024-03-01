using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using Core.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using PersonalWebsiteApi.Core.Cqrs.Queries.Category;
using PersonalWebsiteApi.Core.Cqrs.Responses;
using PersonalWebsiteApi.Core.Entities;
using PersonalWebsiteApi.DataAccess.Interfaces;

namespace PersonalWebsiteApi.Logic.Cqrs.Queries;

public class GetCategoriesQueryHandler : QueryHandler<GetCategoriesQuery, IAsyncEnumerable<GetCategoriesQueryResponse>>
{
    public IUnitOfWork UnitOfWork { get; }

    public GetCategoriesQueryHandler(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    protected override Result<IAsyncEnumerable<GetCategoriesQueryResponse>> Fetch(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Category, bool>> filter = (Category entity) => true;   
        if (request.Language is not null)
        {
            filter = filter.And(entity => entity.Language == request.Language);
        }
        return UnitOfWork.CategoriesRepository.GetAsync(filter, (categroy) => categroy.Adapt<GetCategoriesQueryResponse>());
    }
}
