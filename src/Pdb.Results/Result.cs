namespace Pdb.Results
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Result
    {
        private readonly List<string> _errors = new();

        private readonly List<ValidationError> _validationErrors = new();

        protected Result(ResultStatus status)
        {
            Status = status;
        }

        public ResultStatus Status { get; }

        public IReadOnlyList<string> Errors => _errors;

        public IReadOnlyList<ValidationError> ValidationErrors => _validationErrors;

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

        public static Result Invalid(params ValidationError[] validationErrors) =>
            new InvalidResult(validationErrors);

        public static Result Invalid(IEnumerable<ValidationError> validationErrors) =>
            new InvalidResult(validationErrors);

        public static Result<T> Invalid<T>(params ValidationError[] validationError) =>
             new InvalidResult<T>(validationError);

        public static Result<T> Invalid<T>(IEnumerable<ValidationError> validationErrors) =>
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

            _errors.AddRange(errors);
        }

        protected void AddValidationError(ValidationError validationError)
        {
            if (validationError is null)
            {
                throw new ArgumentNullException(nameof(validationError));
            }

            _validationErrors.Add(validationError);
        }

        protected void AddValidationErrors(IEnumerable<ValidationError> validationErrors)
        {
            if (validationErrors == null)
            {
                throw new ArgumentNullException(nameof(validationErrors));
            }

            foreach (var validationError in validationErrors)
            {
                AddValidationError(validationError);
            }
        }
    }
}