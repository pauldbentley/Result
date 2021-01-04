namespace Pdb.Results
{
    using System;
    using System.Collections.Generic;

    public class Result
    {
        private readonly List<string> _errors = new List<string>();

        protected Result(ResultStatus status)
        {
            Status = status;
        }

        public ResultStatus Status { get; }

        public IEnumerable<string> Errors => _errors;

        public IReadOnlyDictionary<string, string[]> ValidationErrors { get; protected set; } = new Dictionary<string, string[]>();

        public bool IsSuccessful => Status == ResultStatus.Ok;

        public object Problem { get; protected set; } = default!;

        public static Result Ok() =>
            new OkResult();

        public static Result<T> Ok<T>(T value) =>
            new OkResult<T>(value);

        public static Result Error() =>
            new ErrorResult();

        public static Result Error(params string[] errors) =>
            new ErrorResult(errors);

        public static Result Error(IEnumerable<string> errors) =>
            new ErrorResult(errors);

        public static Result Error(object problem) =>
            new ErrorResult(problem);

        public static Result<T> Error<T>() =>
            new ErrorResult<T>();

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

        public static Result Invalid(string error) =>
            Invalid(string.Empty, error);

        public static Result Invalid(params string[] errors) =>
            new InvalidResult(new Dictionary<string, string[]> { { string.Empty, errors } });

        public static Result Invalid(string key, string error) =>
            new InvalidResult(new Dictionary<string, string[]> { { key, new[] { error } } });

        public static Result Invalid(string key, params string[] errors) =>
            new InvalidResult(new Dictionary<string, string[]> { { key, errors } });

        public static Result Invalid(IReadOnlyDictionary<string, string[]> validationErrors) =>
            new InvalidResult(validationErrors);

        public static Result<T> Invalid<T>(string error) =>
            Invalid<T>(string.Empty, error);

        public static Result<T> Invalid<T>(string key, string error) =>
            new InvalidResult<T>(new Dictionary<string, string[]> { { key, new[] { error } } });

        public static Result<T> Invalid<T>(params string[] errors) =>
            Invalid<T>(string.Empty, errors);

        public static Result<T> Invalid<T>(string key, params string[] errors) =>
            new InvalidResult<T>(new Dictionary<string, string[]> { { key, errors } });

        public static Result<T> Invalid<T>(IReadOnlyDictionary<string, string[]> validationErrors) =>
            new InvalidResult<T>(validationErrors);

        public static Result NotFound() =>
            new NotFoundResult();

        public static Result<T> NotFound<T>() =>
            new NotFoundResult<T>();

        protected void AddErrors(IEnumerable<string> errors)
        {
            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            foreach (string error in errors)
            {
                _errors.Add(error);
            }
        }
    }
}