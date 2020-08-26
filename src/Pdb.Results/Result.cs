namespace Pdb.Results
{
    using System.Collections.Generic;

    public static class Result
    {
        public static OperationResult Ok() =>
            new OkResult();

        public static OperationResult<T> Ok<T>(T value) =>
            new OkResult<T>(value);

        public static OperationResult Error(params string[] errors) =>
            new ErrorResult(errors);

        public static OperationResult Error(IEnumerable<string> errors) =>
            new ErrorResult(errors);

        public static OperationResult Error(object problem) =>
            new ErrorResult(problem);

        public static OperationResult<T> Error<T>(params string[] errors) =>
            new ErrorResult<T>(errors);

        public static OperationResult<T> Error<T>(IEnumerable<string> errors) =>
            new ErrorResult<T>(errors);

        public static OperationResult<T> Error<T>(object problem) =>
            new ErrorResult<T>(problem);

        public static OperationResult Forbidden() =>
            new ForbiddenResult();

        public static OperationResult<T> Forbidden<T>() =>
            new ForbiddenResult<T>();

        public static OperationResult Invalid(string key, string error) =>
            new InvalidResult(new Dictionary<string, string[]> { { key, new[] { error } } });

        public static OperationResult Invalid(string key, params string[] errors) =>
            new InvalidResult(new Dictionary<string, string[]> { { key, errors } });

        public static IResult Invalid(IDictionary<string, string[]> validationErrors) =>
            new InvalidResult(validationErrors);

        public static OperationResult<T> Invalid<T>(string key, string error) =>
            new InvalidResult<T>(new Dictionary<string, string[]> { { key, new[] { error } } });

        public static OperationResult<T> Invalid<T>(string key, params string[] errors) =>
            new InvalidResult<T>(new Dictionary<string, string[]> { { key, errors } });

        public static OperationResult<T> Invalid<T>(IDictionary<string, string[]> validationErrors) =>
            new InvalidResult<T>(validationErrors);

        public static OperationResult NotFound() =>
            new NotFoundResult();

        public static OperationResult<T> NotFound<T>() =>
            new NotFoundResult<T>();
    }
}