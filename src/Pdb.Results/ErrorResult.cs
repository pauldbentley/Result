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
            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            foreach (string error in errors)
            {
                AddError(error);
            }
        }

        public ErrorResult(params string[] errors)
            : base(ResultStatus.Error)
        {
            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            foreach (string error in errors)
            {
                AddError(error);
            }
        }

        public ErrorResult(object problem)
            : base(ResultStatus.Error)
        {
            Problem = problem ?? throw new ArgumentNullException(nameof(problem));
        }
    }
}
