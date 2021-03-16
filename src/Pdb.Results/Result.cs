namespace Pdb.Results
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Result
    {
        public Result(ResultStatus status)
        {
            Status = status;
        }

        public ResultStatus Status { get; }

        public IEnumerable<string> Errors { get; private set; } = new List<string>();

        public IEnumerable<ValidationError> ValidationErrors { get; private set; } = new List<ValidationError>();

        public bool IsSuccessful => Status == ResultStatus.Ok;

        public object Problem { get; private set; } = default!;

        public static Result Ok() => new(ResultStatus.Ok);

        public static Result<T> Ok<T>(T value) =>
            new(ResultStatus.Ok, value);

        public static Result Error(params string[] errors)
        {
            if (errors is null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            if (errors.Length == 0)
            {
                throw new ArgumentException(null, nameof(errors));
            }

            return new(ResultStatus.Error)
            {
                Errors = errors,
            };
        }

        public static Result Error(IEnumerable<string> errors)
        {
            if (errors is null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            if (!errors.Any())
            {
                throw new ArgumentException(null, nameof(errors));
            }

            return new(ResultStatus.Error)
            {
                Errors = errors,
            };
        }

        public static Result Error(object problem)
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

        public static Result<T> Error<T>(params string[] errors)
        {
            if (errors is null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            if (errors.Length == 0)
            {
                throw new ArgumentException(null, nameof(errors));
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

            if (!errors.Any())
            {
                throw new ArgumentException(null, nameof(errors));
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
            new(ResultStatus.Forbidden);

        public static Result<T> Forbidden<T>() =>
            new(ResultStatus.Forbidden);

        public static Result Invalid(params ValidationError[] validationErrors)
        {
            if (validationErrors is null)
            {
                throw new ArgumentNullException(nameof(validationErrors));
            }

            if (validationErrors.Length == 0)
            {
                throw new ArgumentException(null, nameof(validationErrors));
            }

            return new(ResultStatus.Invalid)
            {
                ValidationErrors = validationErrors,
            };
        }

        public static Result Invalid(IEnumerable<ValidationError> validationErrors)
        {
            if (validationErrors is null)
            {
                throw new ArgumentNullException(nameof(validationErrors));
            }

            if (!validationErrors.Any())
            {
                throw new ArgumentException(null, nameof(validationErrors));
            }

            return new(ResultStatus.Invalid)
            {
                ValidationErrors = validationErrors,
            };
        }

        public static Result<T> Invalid<T>(params ValidationError[] validationErrors)
        {
            if (validationErrors is null)
            {
                throw new ArgumentNullException(nameof(validationErrors));
            }

            if (validationErrors.Length == 0)
            {
                throw new ArgumentException(null, nameof(validationErrors));
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

            if (!validationErrors.Any())
            {
                throw new ArgumentException(null, nameof(validationErrors));
            }

            return new(ResultStatus.Invalid)
            {
                ValidationErrors = validationErrors,
            };
        }

        public static Result NotFound() =>
            new(ResultStatus.NotFound);

        public static Result<T> NotFound<T>() =>
            new(ResultStatus.NotFound);
    }
}