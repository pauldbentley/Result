namespace Pdb.Results
{
    public class NotFoundResult<T> : OperationResult<T>
    {
        public NotFoundResult()
            : base(ResultStatus.NotFound)
        {
        }
    }
}
