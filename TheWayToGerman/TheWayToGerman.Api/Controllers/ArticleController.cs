using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheWayToGerman.Core.Cqrs.Commands.Admin;
namespace TheWayToGerman.Api.Controllers
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
        // [Authorize(AuthPolicies.OwnerPolicy)]
        public async Task<ActionResult> CreateArticle([FromBody] CreateAdminCommand DTO)
        {

            var result = await Mediator.Send(DTO);
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
        /*  [HttpGet]
         public async Task<ActionResult> GetArticles([FromBody] GetArticlesDTO DTO)
         {
             var userCommand = DTO.Adapt<>();
             var result = await Mediator.Send(userCommand);
             if (result.IsInternalError())
             {
                 return Problem(result.GetErrorMessage());
             }
             if (result.ContainError())
             {
                 return Problem(result.GetErrorMessage(), statusCode: StatusCodes.Status400BadRequest);
             }
             return Ok(result.GetData().Adapt<>());
         }
         [HttpGet]
         [Route("{id}")]
         public async Task<ActionResult> GetArticle([FromQuery] GetArticleDTO DTO)
         {
             var userCommand = DTO.Adapt<>();
             var result = await Mediator.Send(userCommand);
             if (result.IsInternalError())
             {
                 return Problem(result.GetErrorMessage());
             }
             if (result.ContainError())
             {
                 return Problem(result.GetErrorMessage(), statusCode: StatusCodes.Status400BadRequest);
             }
             return Ok(result.GetData().Adapt<>());
         }*/
    }
}
