namespace Pdb.Results
{
    public class NotFoundResult<T> : ResultBase<T>
    {
        public NotFoundResult()
            : base(ResultStatus.NotFound)
        {
        }
    }
}
