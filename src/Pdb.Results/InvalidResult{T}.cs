﻿namespace Pdb.Results
{
    using System;
    using System.Collections.Generic;

    public class InvalidResult<T> : OperationResult<T>
    {
        public InvalidResult(IDictionary<string, string[]> validationErrors)
            : base(ResultStatus.Invalid)
        {
            ValidationErrors = validationErrors ?? throw new ArgumentNullException(nameof(validationErrors));
        }
    }
}