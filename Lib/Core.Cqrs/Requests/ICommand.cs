using Core.DataKit.Result;
using MediatR;

namespace Core.Cqrs.Requests;

public interface ICommand<Data> : IRequest<Result<Data>>
{

}
