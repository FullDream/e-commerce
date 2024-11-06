using Application.Common.Enums;
using MediatR;

namespace Application.Common.Queries;

public record FindAllQuery<TResult>(string[] Select, string[] Include, Dictionary<string, SortOrder> Sorting)
	: IRequest<List<TResult>>;