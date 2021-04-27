namespace Pdb.Results.AspNetCore
{
    using System;

    public class ResultContext
    {
        public static ResultContext Create(Result result) =>
            new(result)
            {
                IsValueResult = false,
            };

        public static ResultContext Create<T>(Result<T> result) =>
            new(result)
            {
                IsValueResult = true,
                Value = result.Value,
                ValueType = typeof(T),
            };

        private ResultContext(ResultBase result)
        {
            Result = result;
        }

        public ResultBase Result { get; private set; }

        public object? Value { get; private set; }

        public Type? ValueType { get; private set; }

        public bool IsValueResult { get; private set; }
    }
}
