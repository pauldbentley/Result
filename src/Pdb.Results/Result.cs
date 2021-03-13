namespace Pdb.Results
{
    using System;
    using System.Collections.Generic;

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

        public static Result Error() =>
            new ErrorResult();

        public static Result Error(string error) =>
            new ErrorResult(error);

        public static Result Error(params string[] errors) =>
            new ErrorResult(errors);

        public static Result Error(IEnumerable<string> errors) =>
            new ErrorResult(errors);

        public static Result Error(object problem) =>
            new ErrorResult(problem);

        public static Result<T> Error<T>() =>
            new ErrorResult<T>();

        public static Result<T> Error<T>(string error) =>
            new ErrorResult<T>(error);

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

        public static Result Invalid() =>
            new InvalidResult();

        public static Result Invalid(string error) =>
            new InvalidResult(error);

        public static Result Invalid(string identifier, string error) =>
            new InvalidResult(identifier, error);

        public static Result Invalid(ValidationError validationError) =>
             new InvalidResult(validationError);

        public static Result Invalid(IEnumerable<ValidationError> validationErrors) =>
            new InvalidResult(validationErrors);

        public static Result<T> Invalid<T>() =>
            new InvalidResult<T>();

        public static Result<T> Invalid<T>(string error) =>
            new InvalidResult<T>(error);

        public static Result<T> Invalid<T>(string identifier, string error) =>
            new InvalidResult<T>(identifier, error);

        public static Result<T> Invalid<T>(ValidationError validationError) =>
             new InvalidResult<T>(validationError);

        public static Result<T> Invalid<T>(IEnumerable<ValidationError> validationErrors) =>
            new InvalidResult<T>(validationErrors);

        public static Result NotFound() =>
            new NotFoundResult();

        public static Result<T> NotFound<T>() =>
            new NotFoundResult<T>();

        protected void AddError(string error)
        {
            _errors.Add(error);
        }

        protected void AddErrors(IEnumerable<string> errors)
        {
            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            foreach (string error in errors)
            {
                AddError(error);
            }
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