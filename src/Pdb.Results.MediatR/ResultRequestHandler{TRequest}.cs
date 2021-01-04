namespace MediatR
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Pdb.Results;

    public abstract class ResultRequestHandler<TRequest> : IRequestHandler<TRequest, Result>
        where TRequest : IRequest<Result>
    {
        public abstract Task<Result> Handle(TRequest request, CancellationToken cancellationToken);

        public Result Error(params string[] errors) =>
            Result.Error(errors);

        public Result Error(IEnumerable<string> errors) =>
            Result.Error(errors);

        public Result Error(object problem) =>
            Result.Error(problem);

        public Result Forbidden() =>
            Result.Forbidden();

        public Result Invalid(IDictionary<string, string[]> validationErrors) =>
            Result.Invalid(validationErrors);

        public Result Invalid(string error) =>
            Result.Invalid(error);

        public Result Invalid(string key, string error) =>
            Result.Invalid(key, error);

        public Result Invalid(string key, params string[] errors) =>
            Result.Invalid(key, errors);

        public Result Invalid(params string[] errors) =>
            Result.Invalid(errors);

        public Result NotFound() =>
            Result.NotFound();

        public Result Ok() =>
            Result.Ok();
    }
}
