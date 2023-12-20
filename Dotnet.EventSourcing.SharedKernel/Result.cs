using System;
namespace Dotnet.EventSourcing.SharedKernel
{
	public class Result<DEvent>
	{
		private Result(DEvent value)
		{
			Value = value;
		}

		public static Result<DEvent> Create(DEvent value)
		{
			return new Result<DEvent>(value);
		}

        public DEvent Value { get; }

		public DEvent GetValue() => Value;




		private readonly List<IError> _errors = new();

		public bool HasError => _errors.Count > 0;

		public List<IError> ErrorDomainEvents => _errors.ToList();

		public Result<DEvent> AddError(IError error)
		{
            _errors.Add(error);
			return this;
		}

    }
}

