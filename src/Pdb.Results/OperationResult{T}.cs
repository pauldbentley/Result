namespace Pdb.Results
{
    public abstract class OperationResult<T> : OperationResult, IResult<T>
    {
        protected OperationResult(ResultStatus status)
            : base(status)
        {
        }

        public T Value { get; protected set; } = default!;

        public static implicit operator T(OperationResult<T> result) => result.Value;

        public static implicit operator OperationResult<T>(T value) => new OkResult<T>(value);
    }
}