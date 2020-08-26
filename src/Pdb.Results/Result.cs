namespace Pdb.Results
{
    using System.Collections.Generic;

    public static class Result
    {
        public static IResult Ok() =>
            new OkResult();

        public static IResult<T> Ok<T>(T value) =>
            new OkResult<T>(value);

        public static IResult Error(params string[] errors) =>
            new ErrorResult(errors);

        public static IResult Error(IEnumerable<string> errors) =>
            new ErrorResult(errors);

        public static IResult Error(object problem) =>
            new ErrorResult(problem);

        public static IResult<T> Error<T>(params string[] errors) =>
            new ErrorResult<T>(errors);

        public static IResult<T> Error<T>(IEnumerable<string> errors) =>
            new ErrorResult<T>(errors);

        public static IResult<T> Error<T>(object problem) =>
            new ErrorResult<T>(problem);

        public static IResult Forbidden() =>
            new ForbiddenResult();

        public static IResult<T> Forbidden<T>() =>
            new ForbiddenResult<T>();

        public static IResult Invalid(string key, string error) =>
            new InvalidResult(new Dictionary<string, string[]> { { key, new[] { error } } });

        public static IResult Invalid(string key, params string[] errors) =>
            new InvalidResult(new Dictionary<string, string[]> { { key, errors } });

        public static IResult Invalid(IDictionary<string, string[]> validationErrors) =>
            new InvalidResult(validationErrors);

        public static IResult<T> Invalid<T>(string key, string error) =>
            new InvalidResult<T>(new Dictionary<string, string[]> { { key, new[] { error } } });

        public static IResult<T> Invalid<T>(string key, params string[] errors) =>
            new InvalidResult<T>(new Dictionary<string, string[]> { { key, errors } });

        public static IResult<T> Invalid<T>(IDictionary<string, string[]> validationErrors) =>
            new InvalidResult<T>(validationErrors);

        public static IResult NotFound() =>
            new NotFoundResult();

        public static IResult<T> NotFound<T>() =>
            new NotFoundResult<T>();
    }
}