namespace Pdb.Results
{
    public class NotFoundResult : OperationResult
    {
        public NotFoundResult()
            : base(ResultStatus.NotFound)
        {
        }
    }
}
