namespace Pdb.Results
{
    using System.Collections.Generic;

    public abstract class ResultBase
    {
        protected ResultBase(ResultStatus status)
        {
            Status = status;
        }

        public ResultStatus Status { get; }

        public IEnumerable<string> Errors { get; internal set; } = new List<string>();

        public IEnumerable<ValidationError> ValidationErrors { get; internal set; } = new List<ValidationError>();

        public bool IsSuccessful => Status == ResultStatus.Ok;

        public object Problem { get; internal set; } = default!;

        public override string ToString() =>
            Status.ToString();
    }
}