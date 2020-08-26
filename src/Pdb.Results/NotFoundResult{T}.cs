namespace Pdb.Results
{
    public class NotFoundResult<T> : Result<T>
    {
        public NotFoundResult()
            : base(ResultStatus.NotFound)
        {
        }
    }
}
