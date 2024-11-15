using Api.QueryParams;
using FluentValidation;

namespace Api.Validators;

public interface IQueryOptionsValidator<T> : IValidator<QueryOptions<T>>;