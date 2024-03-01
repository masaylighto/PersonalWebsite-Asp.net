using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsiteApi.Core.Cqrs.Commands.Article;
using PersonalWebsiteApi.Core.Cqrs.Queries.Article;
using PersonalWebsiteApi.Core.Helpers;

namespace PersonalWebsiteApi.Api.Controllers
{
    [ApiController]
    [Route("api/v1/Article")]
    public class ArticleController : Controller
    {
        public IMediator Mediator { get; }
        public ArticleController(IMediator mediator)
        {
            Mediator = mediator;
        }
        [HttpPost]
        [Authorize(AuthPolicies.AdminsAndOwners)]
        public async Task<ActionResult> CreateArticle([FromBody] CreateArticleCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.IsInternalError())
            {
                return Problem(result.GetErrorMessage());
            }
            if (result.ContainError())
            {
                return Problem(result.GetErrorMessage(), statusCode: StatusCodes.Status400BadRequest);
            }
            return Ok(result.GetData());
        }
        [HttpPut]
        [Authorize(AuthPolicies.AdminsAndOwners)]
        public async Task<ActionResult> UpdateArticle([FromBody] UpdateArticleCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.IsInternalError())
            {
                return Problem(result.GetErrorMessage());
            }
            if (result.ContainError())
            {
                return Problem(result.GetErrorMessage(), statusCode: StatusCodes.Status400BadRequest);
            }
            return Ok();
        }
        [HttpGet]
         public async Task<ActionResult> GetArticles([FromQuery] GetArticlesQuery command)
         {            
             var result = await Mediator.Send(command);
             if (result.IsInternalError())
             {
                 return Problem(result.GetErrorMessage());
             }
             if (result.ContainError())
             {
                 return Problem(result.GetErrorMessage(), statusCode: StatusCodes.Status400BadRequest);
             }
             return Ok(result.GetData());
         }
         [HttpGet]
         [Route("{ID:guid}")]
         public async Task<ActionResult> GetArticle(Guid ID)
         {        
             var result = await Mediator.Send(new GetArticleQuery { ID = ID });
             if (result.IsInternalError())
             {
                 return Problem(result.GetErrorMessage());
             }
             if (result.ContainError())
             {
                 return Problem(result.GetErrorMessage(), statusCode: StatusCodes.Status400BadRequest);
             }
             return Ok(result.GetData());
         }
    }
}
