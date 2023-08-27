
using Core.Cqrs.Handlers;
using Core.DataKit;
using Core.DataKit.Result;
using Core.LinqExtensions;
using FluentValidation;
using HtmlAgilityPack;
using Serilog;
using TheWayToGerman.Core.Cqrs.Commands.Admin;
using TheWayToGerman.Core.Cqrs.Commands.Article;
using TheWayToGerman.Core.Cqrs.Responses;
using TheWayToGerman.Core.Entities;
using TheWayToGerman.Core.Exceptions;
using TheWayToGerman.Core.Helpers;
using TheWayToGerman.Core.Loggers;
using TheWayToGerman.DataAccess.Interfaces;
using Entities=TheWayToGerman.Core.Entities;
namespace TheWayToGerman.Logic.Cqrs.Commands.Article;

public class UpdateArticleCommandHandler : CommandHandler<UpdateArticleCommand, OK>
{
    public IUnitOfWork UnitOfWork { get; }
    public ArticlesHandler ArticlesHandler { get; }
    public ILog Log { get; }

    class CommandValidator : AbstractValidator<UpdateArticleCommand>
    {
        public CommandValidator()
        {
            RuleFor(x => x.ID).NotEmpty();
            RuleFor(x => x.Title).MinimumLength(1);
            RuleFor(x => x.Overview).MinimumLength(20);
            RuleFor(x => x.Content).MinimumLength(100);
            RuleFor(x => x.CategoryID).NotEmpty();
        }
    }
    public UpdateArticleCommandHandler(IUnitOfWork unitOfWork, ArticlesHandler articlesHandler,ILog log)
    {
        Validator = new CommandValidator();
        UnitOfWork = unitOfWork;
        ArticlesHandler = articlesHandler;
        Log = log;
    }
    protected override async Task<Result<OK>> Execute(UpdateArticleCommand request, CancellationToken cancellationToken)
    {

        Guid ID = Guid.NewGuid();
        var oldArticleResult = await UnitOfWork.ArticleRepository.GetAsync(request.ID);
        if (oldArticleResult.ContainError())
        {
            return oldArticleResult.GetError();
        }
        var oldArticle = oldArticleResult.GetData();

        ArticlesHandler.LoadArticleContent(oldArticle.Content);
        await ArticlesHandler.DeleteOldImage();

        var categoryResult = await UnitOfWork.CategoriesRepository.GetAsync(x => x.Id == request.CategoryID);
        if (categoryResult.ContainError())
        {
            return categoryResult.GetError();
        }

        ArticlesHandler.LoadArticleContent(request.Content);

        var seperatedImageResult = await ArticlesHandler.SeparateImage();        
        if (seperatedImageResult.IsNotOk())
        {
            Log.Error(seperatedImageResult.GetError());
            return seperatedImageResult.GetError();
        }

        var articleContentResult =  ArticlesHandler.GetArticleContent();
        if (articleContentResult.ContainError())
        {
            Log.Error(articleContentResult.GetError());
            return articleContentResult.GetError();
        }

        Entities.Article article = new()
        {
            Title = request.Title,
            Overview = request.Overview,
            Content = articleContentResult.GetData(),
            Id = ID,
            Auther = oldArticle.Auther,
            Category = categoryResult.GetData(),
        };

        var result = await UnitOfWork.ArticleRepository.UpdateAsync(article);
        if (result.IsNotOk())
        {
            return result.GetError();
        }
        var saveResult=await UnitOfWork.SaveAsync();
        if (saveResult.ContainError())
        {
            return saveResult.GetError();
        }
        return new OK();
    }
    
}
