using System;
namespace Dotnet.EventSourcing.SharedKernel
{
	public class Result<T>
	{
		private Result(T value)
		{
			Value = value;
		}

		public static Result<T> Create(T value)
		{
			return new Result<T>(value);
		}

        public T Value { get; }

		public T GetValue() => Value;




		private readonly List<IError> _errors = new();

		public bool HasError => _errors.Count > 0;

		public List<IError> Errors => _errors.ToList();

		public Result<T> AddError(IError error)
		{
			_errors.Add(error);
			return this;
		}

    }
}

