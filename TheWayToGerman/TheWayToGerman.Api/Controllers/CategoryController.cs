using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheWayToGerman.Core.Cqrs.Commands;
using TheWayToGerman.Core.Helpers;

namespace TheWayToGerman.Api.Controllers;
[ApiController]
[Route("api/v1/Category")]
public class CategoryController : ControllerBase
{
    public IMediator Mediator { get; }
    public CategoryController(IMediator mediator)
    {
        Mediator = mediator;
    }
    [HttpPost]
    [Authorize(AuthPolicies.OwnerPolicy)]
    public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryCommand userCommand)
    {
        var result = await Mediator.Send(userCommand);
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
    public async Task<ActionResult> GetCategories([FromBody] CreateCategoryCommand userCommand)
    {
        var result = await Mediator.Send(userCommand);
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
