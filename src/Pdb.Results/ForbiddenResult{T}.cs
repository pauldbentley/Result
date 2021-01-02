namespace Pdb.Results
{
    internal class ForbiddenResult<T> : Result<T>
    {
        public ForbiddenResult()
            : base(ResultStatus.Forbidden)
        {
        }
    }
}
