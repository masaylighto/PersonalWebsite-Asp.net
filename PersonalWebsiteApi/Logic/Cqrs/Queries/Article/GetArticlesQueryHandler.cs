using Core.Cqrs.Handlers;
using Core.DataKit.Result;
using Core.Expressions;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using PersonalWebsiteApi.Core.Cqrs.Commands.Article;
using PersonalWebsiteApi.Core.Cqrs.Queries.Article;
using PersonalWebsiteApi.Core.Cqrs.Queries.Category;
using PersonalWebsiteApi.Core.Cqrs.Responses;
using PersonalWebsiteApi.Core.Entities;
using PersonalWebsiteApi.DataAccess.Interfaces;

namespace PersonalWebsiteApi.Logic.Cqrs.Queries;

public class GetArticlesQueryHandler : QueryHandler<GetArticlesQuery, IAsyncEnumerable<GetArticlesQueryResponse>>
{
    public IUnitOfWork UnitOfWork { get; }

    class CommandValidator : AbstractValidator<GetArticlesQuery>
    {
        public CommandValidator()
        {
            RuleFor(x => x.PageLength).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
            
        }
    }
    public GetArticlesQueryHandler(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
        Validator = new CommandValidator();
    }

    protected override Result<IAsyncEnumerable<GetArticlesQueryResponse>> Fetch(GetArticlesQuery request, CancellationToken cancellationToken)
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
        return UnitOfWork.ArticleRepository.GetAsync(predicate, GetArticlesQueryResponse.From, request.PageNumber, request.PageLength);
    }
}
