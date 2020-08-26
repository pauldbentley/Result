namespace Pdb.Results
{
    public interface IResult<T> : IResult
    {
        T Value { get; }
    }
}