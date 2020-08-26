namespace Pdb.Results
{
    public class ForbiddenResult<T> : ResultBase<T>
    {
        public ForbiddenResult()
            : base(ResultStatus.Forbidden)
        {
        }
    }
}
