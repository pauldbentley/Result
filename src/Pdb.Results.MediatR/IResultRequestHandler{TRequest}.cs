namespace MediatR
{
    using Pdb.Results;

    public interface IResultRequestHandler<TRequest> : IRequestHandler<TRequest, IResult>
        where TRequest : IRequest<IResult>
    {
    }
}
