using System;
namespace Dotnet.EventSourcing.SharedKernel
{
	public class Result<Entity> where Entity : new()
	{
		private Result(Entity value)
		{
			EntityValue = value;
		}

		private Result()
		{
			EntityValue = new();
		}

		public static Result<Entity> Create(Entity value)
		{
			return new Result<Entity>(value);
		}

		public static Result<Entity> Create()
		{
			return new Result<Entity>();
		}

		public Entity EntityValue { get; private set; }

		private readonly List<IError> _domainErrors = new();

		public bool HasError => _domainErrors.Count > 0;

		public List<IError> DomainErrors => _domainErrors.ToList();

		public Result<Entity> AddError(IError error)
		{
			_domainErrors.Add(error);
			return this;
		}

		public Result<Entity> AddErrorIf(Func<bool> predicate, IError errorToBeAddIfTrue)
		{
			if (predicate()) AddError(errorToBeAddIfTrue);
			return this;
		}

		public void UpdateValueIfNoError(Entity value)
		{
			if (!HasError) EntityValue = value;
		}

	}
}

