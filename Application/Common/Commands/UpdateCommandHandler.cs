using AutoMapper;
using Core.Interfaces;
using MediatR;

namespace Application.Common.Commands;

internal class UpdateCommandHandler<TEntity, TDto, TResult>(IRepository<TEntity> repository, IMapper mapper)
	: IRequestHandler<UpdateCommand<TDto, TResult>, TResult>
	where TEntity : class, IEntity
{
	public async Task<TResult> Handle(UpdateCommand<TDto, TResult> request, CancellationToken cancellationToken)
	{
		var entity = mapper.Map<TEntity>(request.Dto);
		var created = await repository.CreateAsync(entity, cancellationToken);

		return mapper.Map<TResult>(created);
	}
}