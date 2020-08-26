namespace Pdb.Results
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ErrorResult : ResultBase
    {
        public ErrorResult(IEnumerable<string> errors)
            : base(ResultStatus.Error)
        {
            Errors = errors ?? Enumerable.Empty<string>();
        }

        public ErrorResult(params string[] errors)
            : base(ResultStatus.Error)
        {
            Errors = errors;
        }

        public ErrorResult(object problem)
            : base(ResultStatus.Error)
        {
            Problem = problem ?? throw new ArgumentNullException(nameof(problem));
        }
    }
}
