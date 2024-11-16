using Api.QueryParams;
using FluentValidation;

namespace Api.Validators;

public interface IListQueryOptionsValidator<T> : IValidator<ListQueryOptions<T>>;