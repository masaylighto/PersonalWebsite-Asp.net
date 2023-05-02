using Core.Cqrs.Requests;
using Cqrs.Handlers;

namespace Core.Cqrs.Handlers;

public abstract class CommandHandler<Command, Response> : BaseHandler<Command, Response> where Command : ICommand<Response>
{

}
