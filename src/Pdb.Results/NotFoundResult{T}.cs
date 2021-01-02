namespace Pdb.Results
{
    internal class NotFoundResult<T> : Result<T>
    {
        public NotFoundResult()
            : base(ResultStatus.NotFound)
        {
        }
    }
}
