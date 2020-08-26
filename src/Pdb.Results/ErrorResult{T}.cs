namespace Pdb.Results
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ErrorResult<T> : Result<T>
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

        public ErrorResult(object error)
            : base(ResultStatus.Error)
        {
            Problem = error ?? throw new ArgumentNullException(nameof(error));
        }
    }
}
