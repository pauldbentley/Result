namespace Pdb.Results
{
    public class Result<T> : Result
    {
        protected Result(ResultStatus status)
            : base(status)
        {
        }

        public T Value { get; protected set; } = default!;

        public static implicit operator Result<T>(T value)
        {
            if (value != null)
            {
                return new OkResult<T>(value);
            }

            return new NotFoundResult<T>();
        }

        public static implicit operator T(Result<T> result) => result.Value;
    }
}