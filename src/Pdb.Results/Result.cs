namespace Pdb.Results
{
    using System.Collections.Generic;

    public class Result : IResult
    {
        protected Result(ResultStatus status)
        {
            Status = status;
        }

        public ResultStatus Status { get; }

        public IEnumerable<string> Errors { get; protected set; } = new List<string>();

        public IDictionary<string, string[]> ValidationErrors { get; protected set; } = new Dictionary<string, string[]>();

        public bool IsSuccessful => Status == ResultStatus.Ok;

        public object Problem { get; protected set; } = default!;

        public static Result Ok() =>
            new OkResult();

        public static Result<T> Ok<T>(T value) =>
            new OkResult<T>(value);

        public static Result Error(params string[] errors) =>
            new ErrorResult(errors);

        public static Result Error(IEnumerable<string> errors) =>
            new ErrorResult(errors);

        public static Result Error(object problem) =>
            new ErrorResult(problem);

        public static Result<T> Error<T>(params string[] errors) =>
            new ErrorResult<T>(errors);

        public static Result<T> Error<T>(IEnumerable<string> errors) =>
            new ErrorResult<T>(errors);

        public static Result<T> Error<T>(object problem) =>
            new ErrorResult<T>(problem);

        public static Result Forbidden() =>
            new ForbiddenResult();

        public static Result<T> Forbidden<T>() =>
            new ForbiddenResult<T>();

        public static Result Invalid(string key, string error) =>
            new InvalidResult(new Dictionary<string, string[]> { { key, new[] { error } } });

        public static Result Invalid(string key, params string[] errors) =>
            new InvalidResult(new Dictionary<string, string[]> { { key, errors } });

        public static Result Invalid(IDictionary<string, string[]> validationErrors) =>
            new InvalidResult(validationErrors);

        public static Result<T> Invalid<T>(string key, string error) =>
            new InvalidResult<T>(new Dictionary<string, string[]> { { key, new[] { error } } });

        public static Result<T> Invalid<T>(string key, params string[] errors) =>
            new InvalidResult<T>(new Dictionary<string, string[]> { { key, errors } });

        public static Result<T> Invalid<T>(IDictionary<string, string[]> validationErrors) =>
            new InvalidResult<T>(validationErrors);

        public static Result NotFound() =>
            new NotFoundResult();

        public static Result<T> NotFound<T>() =>
            new NotFoundResult<T>();
    }
}