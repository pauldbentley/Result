namespace MediatR
{
    using Pdb.Results;

    public interface IResultRequest<TResponse> : IRequest<IResult<TResponse>>
    {
    }
}
