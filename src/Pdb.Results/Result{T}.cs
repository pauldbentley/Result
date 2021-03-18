namespace Pdb.Results
{
    using System;
    using System.Collections.Generic;

    public class Result<T>
    {
        internal Result(ResultStatus status)
            : this(status, default!)
        {
        }

        internal Result(ResultStatus status, T value)
        {
            Status = status;
            Value = value;
        }

        public T Value { get; internal set; }

        public ResultStatus Status { get; internal set; }

        public IEnumerable<string> Errors { get; internal set; } = new List<string>();

        public IEnumerable<ValidationError> ValidationErrors { get; internal set; } = new List<ValidationError>();

        public bool IsSuccessful => Status == ResultStatus.Ok;

        public object Problem { get; internal set; } = default!;

        public static implicit operator Result(Result<T> value) => value.ToResult();

        public static implicit operator Result<T>(T value)
        {
            if (value != null)
            {
                return new(ResultStatus.Ok, value);
            }

            return new(ResultStatus.NotFound);
        }

        public static implicit operator T(Result<T> result) => result.Value;

        public override string ToString() => Status.ToString();

        public Result ToResult() =>
            new(Status)
            {
                Errors = Errors,
                Problem = Problem,
                ValidationErrors = ValidationErrors,
            };
    }
}