namespace Pdb.Results
{
    public class ForbiddenResult<T> : Result<T>
    {
        public ForbiddenResult()
            : base(ResultStatus.Forbidden)
        {
        }
    }
}
