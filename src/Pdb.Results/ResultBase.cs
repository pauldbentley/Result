namespace Pdb.Results
{
    using System.Collections.Generic;

    public abstract class ResultBase : IResult
    {
        protected ResultBase(ResultStatus status)
        {
            Status = status;
        }

        public ResultStatus Status { get; }

        public IEnumerable<string> Errors { get; protected set; } = new List<string>();

        public IDictionary<string, string[]> ValidationErrors { get; protected set; } = new Dictionary<string, string[]>();

        public bool IsSuccessful => Status == ResultStatus.Ok;

        public object Problem { get; protected set; } = default!;
    }
}