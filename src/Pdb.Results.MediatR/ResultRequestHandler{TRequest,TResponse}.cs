namespace MediatR
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Pdb.Results;

    public abstract class ResultRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, IResult<TResponse>>
        where TRequest : IRequest<IResult<TResponse>>
    {
        public abstract Task<IResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);

        public IResult<TResponse> Error(params string[] errors) =>
            Result.Error<TResponse>(errors);

        public IResult<TResponse> Error(IEnumerable<string> errors) =>
            Result.Error<TResponse>(errors);

        public IResult<TResponse> Error(object problem) =>
            Result.Error<TResponse>(problem);

        public IResult<TResponse> Forbidden() =>
            Result.Forbidden<TResponse>();

        public IResult<TResponse> Invalid(IDictionary<string, string[]> validationErrors) =>
            Result.Invalid<TResponse>(validationErrors);

        public IResult<TResponse> Invalid(string key, string error) =>
            Result.Invalid<TResponse>(key, error);

        public IResult<TResponse> Invalid(string key, params string[] errors) =>
            Result.Invalid<TResponse>(key, errors);

        public IResult<TResponse> NotFound() =>
            Result.NotFound<TResponse>();

        public IResult<TResponse> Ok(TResponse value) =>
            Result.Ok(value);
    }
}
