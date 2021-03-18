namespace Pdb.Results
{
    using System;
    using System.Collections.Generic;

    public class Result : Result<VoidValue>
    {
        public Result(ResultStatus status)
            : base(status, VoidValue.Value)
        {
        }

        public static Result Ok() =>
            Ok(VoidValue.Value);

        public static Result<T> Ok<T>(T value) =>
            new(ResultStatus.Ok, value);

        public static Result Error(params string[] errors) =>
            Error<VoidValue>(errors);

        public static Result Error(IEnumerable<string> errors) =>
            Error<VoidValue>(errors);

        public static Result Error(object problem) =>
            Error<VoidValue>(problem);

        public static Result<T> Error<T>(params string[] errors)
        {
            if (errors is null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            return new(ResultStatus.Error)
            {
                Errors = errors,
            };
        }

        public static Result<T> Error<T>(IEnumerable<string> errors)
        {
            if (errors is null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            return new(ResultStatus.Error)
            {
                Errors = errors,
            };
        }

        public static Result<T> Error<T>(object problem)
        {
            if (problem is null)
            {
                throw new ArgumentNullException(nameof(problem));
            }

            return new(ResultStatus.Error)
            {
                Problem = problem,
            };
        }

        public static Result Forbidden() =>
            Forbidden<VoidValue>();

        public static Result<T> Forbidden<T>() =>
            new(ResultStatus.Forbidden);

        public static Result Invalid(string identifier, string message) =>
            Invalid<VoidValue>(identifier, message);

        public static Result Invalid(params ValidationError[] validationErrors) =>
            Invalid<VoidValue>(validationErrors);

        public static Result Invalid(IEnumerable<ValidationError> validationErrors) =>
            Invalid<VoidValue>(validationErrors);

        public static Result Invalid(IReadOnlyDictionary<string, string[]> validationErrors) =>
            Invalid<VoidValue>(validationErrors);

        public static Result<T> Invalid<T>(string identifier, string message) =>
            Invalid<T>(new ValidationError(identifier, message));

        public static Result<T> Invalid<T>(params ValidationError[] validationErrors)
        {
            if (validationErrors is null)
            {
                throw new ArgumentNullException(nameof(validationErrors));
            }

            return new(ResultStatus.Invalid)
            {
                ValidationErrors = validationErrors,
            };
        }

        public static Result<T> Invalid<T>(IEnumerable<ValidationError> validationErrors)
        {
            if (validationErrors is null)
            {
                throw new ArgumentNullException(nameof(validationErrors));
            }

            return new(ResultStatus.Invalid)
            {
                ValidationErrors = validationErrors,
            };
        }

        public static Result<T> Invalid<T>(IReadOnlyDictionary<string, string[]> validationErrors)
        {
            if (validationErrors is null)
            {
                throw new ArgumentNullException(nameof(validationErrors));
            }

            var list = new List<ValidationError>();
            foreach (var validationError in validationErrors)
            {
                list.Add(validationError.Key, validationError.Value);
            }

            return Invalid<T>(list);
        }

        public static Result NotFound() =>
            NotFound<VoidValue>();

        public static Result<T> NotFound<T>() =>
            new(ResultStatus.NotFound);
    }
}