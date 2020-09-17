namespace MediatR
{
    using Pdb.Results;

    public interface IResultRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, IResult<TResponse>>
        where TRequest : IRequest<IResult<TResponse>>
    {
    }
}
