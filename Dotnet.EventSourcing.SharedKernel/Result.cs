using System;
namespace Dotnet.EventSourcing.SharedKernel
{
	public class Result<DEvent> where DEvent : new()
	{
		private Result(DEvent value)
		{
			Value = value;
		}

        private Result()
        {
			Value = new();
        }

        public static Result<DEvent> Create(DEvent value)
		{
			return new Result<DEvent>(value);
		}

        public static Result<DEvent> Create()
        {
            return new Result<DEvent>();
        }

        public DEvent Value { get; private set; }

		public DEvent GetValue() => Value;


		private readonly List<IError> _errors = new();

		public bool HasError => _errors.Count > 0;

		public List<IError> ErrorDomainEvents => _errors.ToList();

		public Result<DEvent> AddError(IError error)
		{
            _errors.Add(error);
			return this;
		}

		public Result<DEvent> AddErrorIf(Func<bool> predicate, IError errorToBeAddIfTrue)
		{
			if (predicate())
				AddError(errorToBeAddIfTrue);

			return this;
		}

		public void UpdateValueIfNoError(DEvent value)
		{
			if(!HasError)
				Value = value;
		}

    }
}

