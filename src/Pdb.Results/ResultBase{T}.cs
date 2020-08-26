namespace Pdb.Results
{
    public abstract class ResultBase<T> : ResultBase, IResult<T>
    {
        protected ResultBase(ResultStatus status)
            : base(status)
        {
        }

        public T Value { get; protected set; }
    }
}