namespace Pdb.Results
{
    using System;
    using System.Collections.Generic;

    internal class InvalidResult : Result
    {
        public InvalidResult(IReadOnlyDictionary<string, string[]> validationErrors)
            : base(ResultStatus.Invalid)
        {
            ValidationErrors = validationErrors ?? throw new ArgumentNullException(nameof(validationErrors));
        }
    }
}
