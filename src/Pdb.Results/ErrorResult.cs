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
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }

        public ErrorResult(params string[] errors)
            : base(ResultStatus.Error)
        {
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }

        public ErrorResult(object problem)
            : base(ResultStatus.Error)
        {
            Problem = problem ?? throw new ArgumentNullException(nameof(problem));
        }
    }
}
