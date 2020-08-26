namespace Pdb.Results
{
    public abstract class ResultBase<T> : ResultBase, IResult<T>
    {
        protected ResultBase(ResultStatus status)
            : base(status)
        {
        }

        public T Value { get; protected set; } = default!;

        public static implicit operator T(ResultBase<T> result) => result.Value;
    }
}