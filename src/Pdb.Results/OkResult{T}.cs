﻿namespace Pdb.Results
{
    using System;

    public class OkResult<T> : OperationResult<T>
    {
        public OkResult(T value)
            : base(ResultStatus.Ok)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public OkResult()
            : base(ResultStatus.Ok)
        {
        }
    }
}