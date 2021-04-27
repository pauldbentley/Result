namespace Pdb.Results
{
    using System.Collections.Generic;

    public class Result : ResultBase
    {
        public Result(ResultStatus status)
            : base(status)
        {
        }

        public static Result<T> Outcome<T>(T value) =>
            value == null
                ? NotFound<T>()
                : Ok(value);

        public static Result Ok() =>
            new(ResultStatus.Ok);

        public static Result<T> Ok<T>(T value) =>
            new(ResultStatus.Ok, value);

        public static Result Error(params string[] errors) =>
            new(ResultStatus.Error)
            {
                Errors = errors,
            };

        public static Result Error(IEnumerable<string> errors) =>
            new(ResultStatus.Error)
            {
                Errors = errors,
            };

        public static Result Error(object problem) =>
            new(ResultStatus.Error)
            {
                Problem = problem,
            };

        public static Result<T> Error<T>(params string[] errors) =>
            new(ResultStatus.Error)
            {
                Errors = errors,
            };

        public static Result<T> Error<T>(IEnumerable<string> errors) =>
            new(ResultStatus.Error)
            {
                Errors = errors,
            };

        public static Result<T> Error<T>(object problem) =>
            new(ResultStatus.Error)
            {
                Problem = problem,
            };

        public static Result Forbidden() =>
            new(ResultStatus.Forbidden);

        public static Result<T> Forbidden<T>() =>
            new(ResultStatus.Forbidden);

        public static Result Invalid(string identifier, string message) =>
            Invalid(new ValidationError(identifier, message));

        public static Result Invalid(params ValidationError[] validationErrors) =>
            new(ResultStatus.Invalid)
            {
                ValidationErrors = validationErrors,
            };

        public static Result Invalid(IEnumerable<ValidationError> validationErrors) =>
            new(ResultStatus.Invalid)
            {
                ValidationErrors = validationErrors,
            };

        public static Result Invalid(IReadOnlyDictionary<string, string[]> validationErrors)
        {
            var list = new List<ValidationError>();

            foreach (var validationError in validationErrors)
            {
                list.Add(validationError.Key, validationError.Value);
            }

            return Invalid(list);
        }

        public static Result<T> Invalid<T>(string identifier, string message) =>
            Invalid<T>(new ValidationError(identifier, message));

        public static Result<T> Invalid<T>(params ValidationError[] validationErrors) =>
            new(ResultStatus.Invalid)
            {
                ValidationErrors = validationErrors,
            };

        public static Result<T> Invalid<T>(IEnumerable<ValidationError> validationErrors) =>
            new(ResultStatus.Invalid)
            {
                ValidationErrors = validationErrors,
            };

        public static Result<T> Invalid<T>(IReadOnlyDictionary<string, string[]> validationErrors)
        {
            var list = new List<ValidationError>();

            foreach (var validationError in validationErrors)
            {
                list.Add(validationError.Key, validationError.Value);
            }

            return Invalid<T>(list);
        }

        public static Result NotFound() =>
            new(ResultStatus.NotFound);

        public static Result<T> NotFound<T>() =>
            new(ResultStatus.NotFound);
    }
}