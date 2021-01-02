namespace Pdb.Results
{
    using System;

    internal class OkResult<T> : Result<T>
    {
        public OkResult(T value)
            : base(ResultStatus.Ok)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
