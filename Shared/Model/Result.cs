using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Model
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }

        protected Result(bool isSuccess, string? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Ok() => new Result(true, null);
        public static Result Fail(string error) => new Result(false, error);
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        protected Result(T? value, bool isSuccess, string? error) : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<T> Ok(T value) => new Result<T>(value, true, null);
        public static new Result<T> Fail(string error) => new Result<T>(default, false, error);
    }
}
