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

        public ErrorResult(string error)
            : this()
        {
            AddError(error);
        }

        public ErrorResult(IEnumerable<string> errors)
            : this()
        {
            AddErrors(errors);
        }

        public ErrorResult(object error)
            : this()
        {
            Problem = error ?? throw new ArgumentNullException(nameof(error));
        }
    }
}
