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

        public static OkResult Ok() =>
            new OkResult();

        public static OkResult<T> Ok<T>(T value) =>
            new OkResult<T>(value);

        public static ErrorResult Error(params string[] errors) =>
            new ErrorResult(errors);

        public static ErrorResult Error(IEnumerable<string> errors) =>
            new ErrorResult(errors);

        public static ErrorResult Error(object problem) =>
            new ErrorResult(problem);

        public static ErrorResult<T> Error<T>(params string[] errors) =>
            new ErrorResult<T>(errors);

        public static ErrorResult<T> Error<T>(IEnumerable<string> errors) =>
            new ErrorResult<T>(errors);

        public static ErrorResult<T> Error<T>(object problem) =>
            new ErrorResult<T>(problem);

        public static ForbiddenResult Forbidden() =>
            new ForbiddenResult();

        public static ForbiddenResult<T> Forbidden<T>() =>
            new ForbiddenResult<T>();

        public static InvalidResult Invalid(string key, string error) =>
            new InvalidResult(new Dictionary<string, string[]> { { key, new[] { error } } });

        public static InvalidResult Invalid(string key, params string[] errors) =>
            new InvalidResult(new Dictionary<string, string[]> { { key, errors } });

        public static InvalidResult Invalid(IDictionary<string, string[]> validationErrors) =>
            new InvalidResult(validationErrors);

        public static InvalidResult<T> Invalid<T>(string key, string error) =>
            new InvalidResult<T>(new Dictionary<string, string[]> { { key, new[] { error } } });

        public static InvalidResult<T> Invalid<T>(string key, params string[] errors) =>
            new InvalidResult<T>(new Dictionary<string, string[]> { { key, errors } });

        public static InvalidResult<T> Invalid<T>(IDictionary<string, string[]> validationErrors) =>
            new InvalidResult<T>(validationErrors);

        public static NotFoundResult NotFound() =>
            new NotFoundResult();

        public static NotFoundResult<T> NotFound<T>() =>
            new NotFoundResult<T>();
    }
}