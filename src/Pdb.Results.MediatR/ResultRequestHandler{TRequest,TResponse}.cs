namespace MediatR
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Pdb.Results;

    public abstract class ResultRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
        where TRequest : IRequest<Result<TResponse>>
    {
        public abstract Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);

        public Result<TResponse> Error(params string[] errors) =>
            Result.Error<TResponse>(errors);

        public Result<TResponse> Error(IEnumerable<string> errors) =>
            Result.Error<TResponse>(errors);

        public Result<TResponse> Error(object problem) =>
            Result.Error<TResponse>(problem);

        public Result<TResponse> Forbidden() =>
            Result.Forbidden<TResponse>();

        public Result<TResponse> Invalid(IDictionary<string, string[]> validationErrors) =>
            Result.Invalid<TResponse>(validationErrors);

        public Result<TResponse> Invalid(string key, string error) =>
            Result.Invalid<TResponse>(key, error);

        public Result<TResponse> Invalid(string error) =>
            Result.Invalid<TResponse>(error);

        public Result<TResponse> Invalid(string key, params string[] errors) =>
            Result.Invalid<TResponse>(key, errors);

        public Result<TResponse> Invalid(params string[] errors) =>
            Result.Invalid<TResponse>(errors);

        public Result<TResponse> NotFound() =>
            Result.NotFound<TResponse>();

        public Result<TResponse> Ok(TResponse value) =>
            Result.Ok(value);
    }
}
