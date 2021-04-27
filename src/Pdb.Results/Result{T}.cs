namespace Pdb.Results
{
    public class Result<T> : ResultBase
    {
        public Result()
            : this(ResultStatus.Ok)
        {
        }

        public Result(ResultStatus status)
            : this(status, default!)
        {
        }

        public Result(ResultStatus status, T value)
            : base(status)
        {
            Value = value;
        }

        public T Value { get; protected set; }

        public static implicit operator Result(Result<T> value) => value.ToResult();

        public static implicit operator Result<T>(T value) =>
            Result.Outcome(value);

        public static implicit operator T(Result<T> result) =>
            result.Value;

        public override string ToString() =>
            Status.ToString();

        public Result ToResult() =>
            new(Status)
            {
                Errors = Errors,
                Problem = Problem,
                ValidationErrors = ValidationErrors,
            };
    }
}