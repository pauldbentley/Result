namespace Pdb.Results
{
    using System;
    using System.Collections.Generic;

    internal class ErrorResult<T> : Result<T>
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

        public ErrorResult(object error)
            : base(ResultStatus.Error)
        {
            Problem = error ?? throw new ArgumentNullException(nameof(error));
        }
    }
}
