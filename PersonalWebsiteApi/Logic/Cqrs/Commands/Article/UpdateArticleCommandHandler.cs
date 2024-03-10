
using Core.Cqrs.Handlers;
using Core.DataKit;
using Core.DataKit.Result;
using Core.LinqExtensions;
using FluentValidation;
using HtmlAgilityPack;
using PersonalWebsiteApi.Core.Cqrs.Commands.Article;
using PersonalWebsiteApi.Core.Cqrs.Responses;
using PersonalWebsiteApi.Core.Entities;
using PersonalWebsiteApi.Core.Exceptions;
using PersonalWebsiteApi.Core.Helpers;
using PersonalWebsiteApi.Core.Loggers;
using PersonalWebsiteApi.DataAccess.Interfaces;
using Entities=PersonalWebsiteApi.Core.Entities;
namespace PersonalWebsiteApi.Logic.Cqrs.Commands.Article;

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

        oldArticle.Title = request.Title;
        oldArticle.Overview = request.Overview;
        oldArticle.Content = articleContentResult.GetData();
        oldArticle.Auther = oldArticle.Auther;
        oldArticle.Category = categoryResult.GetData();      

        var result = UnitOfWork.ArticleRepository.SetStateToUpdate(oldArticle);
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
