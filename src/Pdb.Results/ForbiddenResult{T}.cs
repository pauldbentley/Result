namespace Pdb.Results
{
    public class ForbiddenResult<T> : OperationResult<T>
    {
        public ForbiddenResult()
            : base(ResultStatus.Forbidden)
        {
        }
    }
}
