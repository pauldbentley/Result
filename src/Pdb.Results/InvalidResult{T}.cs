namespace Pdb.Results
{
    using System.Collections.Generic;

    internal class InvalidResult<T> : Result<T>
    {
        public InvalidResult()
            : base(ResultStatus.Invalid)
        {
        }

        public InvalidResult(string error)
            : this()
        {
            AddValidationError(new ValidationError(null, error));
        }

        public InvalidResult(string identifer, string error)
            : this()
        {
            AddValidationError(new ValidationError(identifer, error));
        }

        public InvalidResult(ValidationError validationError)
            : this()
        {
            AddValidationError(validationError);
        }

        public InvalidResult(IEnumerable<ValidationError> validationErrors)
            : this()
        {
            AddValidationErrors(validationErrors);
        }
    }
}
