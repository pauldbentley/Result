namespace MediatR
{
    using Pdb.Results;

    public interface IResultRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
        where TRequest : IRequest<Result<TResponse>>
    {
    }
}
