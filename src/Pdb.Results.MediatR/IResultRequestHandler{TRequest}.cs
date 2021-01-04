namespace MediatR
{
    using Pdb.Results;

    public interface IResultRequestHandler<TRequest> : IRequestHandler<TRequest, Result>
        where TRequest : IRequest<Result>
    {
    }
}
