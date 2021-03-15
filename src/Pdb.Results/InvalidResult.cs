namespace Pdb.Results
{
    using System.Collections.Generic;

    internal class InvalidResult : Result
    {
        public InvalidResult(IEnumerable<ValidationError> validationErrors)
            : this()
        {
            AddValidationErrors(validationErrors);
        }

        private InvalidResult()
            : base(ResultStatus.Invalid)
        {
        }
    }
}
