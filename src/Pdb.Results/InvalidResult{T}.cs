namespace Pdb.Results
{
    using System.Collections.Generic;

    internal class InvalidResult<T> : Result<T>
    {
        public InvalidResult()
            : base(ResultStatus.Invalid)
        {
        }

        public InvalidResult(IDictionary<string, string[]> validationErrors)
            : base(ResultStatus.Invalid)
        {
            AddValidationErrors(validationErrors);
        }
    }
}
