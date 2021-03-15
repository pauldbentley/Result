namespace Pdb.Results
{
    using System;
    using System.Collections.Generic;

    internal class ErrorResult : Result
    {
        public ErrorResult(IEnumerable<string> errors)
            : this()
        {
            AddErrors(errors);
        }

        public ErrorResult(object problem)
            : this()
        {
            Problem = problem ?? throw new ArgumentNullException(nameof(problem));
        }

        private ErrorResult()
            : base(ResultStatus.Error)
        {
        }
    }
}
