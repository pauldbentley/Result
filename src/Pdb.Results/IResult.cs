namespace Pdb.Results
{
    using System.Collections.Generic;

    public interface IResult
    {
        ResultStatus Status { get; }

        IEnumerable<string> Errors { get; }

        object? Problem { get; }

        IDictionary<string, string[]> ValidationErrors { get; }

        bool IsSuccessful { get; }
    }
}