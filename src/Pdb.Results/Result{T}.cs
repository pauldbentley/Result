namespace Pdb.Results
{
    using System;

    public class Result<T> : Result
    {
        public Result(ResultStatus status)
            : base(status)
        {
            if (status == ResultStatus.Ok)
            {
                throw new InvalidOperationException();
            }

            Value = default!;
        }

        public Result(ResultStatus status, T value)
            : base(status)
        {
            if (status == ResultStatus.Ok && value is null)
            {
                throw new InvalidOperationException();
            }

            Value = value;
        }

        public T Value { get; private set; }

        public static implicit operator Result<T>(T value)
        {
            if (value != null)
            {
                return new(ResultStatus.Ok, value);
            }

            return new(ResultStatus.NotFound);
        }

        public static implicit operator T(Result<T> result) => result.Value;
    }
}