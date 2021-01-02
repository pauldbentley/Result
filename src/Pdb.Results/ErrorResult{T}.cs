﻿namespace Pdb.Results
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class ErrorResult<T> : Result<T>
    {
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

        public ErrorResult(object error)
            : base(ResultStatus.Error)
        {
            Problem = error ?? throw new ArgumentNullException(nameof(error));
        }
    }
}
