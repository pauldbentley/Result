namespace Pdb.Results
{
    using System;
    using System.Collections.Generic;

    internal class ErrorResult : Result
    {
        public ErrorResult()
            : base(ResultStatus.Error)
        {
        }

        public ErrorResult(IEnumerable<string> errors)
            : base(ResultStatus.Error)
        {
            AddErrors(errors);
        }

        public ErrorResult(object problem)
            : base(ResultStatus.Error)
        {
            Problem = problem ?? throw new ArgumentNullException(nameof(problem));
        }
    }
}
