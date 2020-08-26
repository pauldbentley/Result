namespace Pdb.Results
{
    using System;

    public class OkResult<T> : ResultBase<T>
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

        public static implicit operator OkResult<T>(T value) => new OkResult<T>(value);
    }
}
