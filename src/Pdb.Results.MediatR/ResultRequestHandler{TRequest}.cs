namespace MediatR
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Pdb.Results;

    public abstract class ResultRequestHandler<TRequest> : IRequestHandler<TRequest, IResult>
        where TRequest : IRequest<IResult>
    {
        public abstract Task<IResult> Handle(TRequest request, CancellationToken cancellationToken);

        public IResult Error(params string[] errors) =>
            Result.Error(errors);

        public IResult Error(IEnumerable<string> errors) =>
            Result.Error(errors);

        public IResult Error(object problem) =>
            Result.Error(problem);

        public IResult Forbidden() =>
            Result.Forbidden();

        public IResult Invalid(IDictionary<string, string[]> validationErrors) =>
            Result.Invalid(validationErrors);

        public IResult Invalid(string error) =>
            Result.Invalid(error);

        public IResult Invalid(string key, string error) =>
            Result.Invalid(key, error);

        public IResult Invalid(string key, params string[] errors) =>
            Result.Invalid(key, errors);

        public IResult Invalid(params string[] errors) =>
            Result.Invalid(errors);

        public IResult NotFound() =>
            Result.NotFound();

        public IResult Ok() =>
            Result.Ok();
    }
}
