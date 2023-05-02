
using Core.DataKit.Result;
using FluentValidation;
using MediatR;


namespace Cqrs.Handlers;

public abstract class BaseHandler<Request, Response> : IRequestHandler<Request,Result<Response>> where Request : IRequest<Result<Response>>
{
    protected IValidator<Request>? Validator { get; set; }

    protected abstract Task<Result<Response>> Execute(Request request, CancellationToken cancellationToken);

    public async Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
    {
        if (Validator is not null)
        {
            var validationResult = Validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new ValidationException(validationResult.Errors);
            }
        }
        return await Execute(request, cancellationToken);
    }
}
