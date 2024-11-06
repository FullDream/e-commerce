using Application.Product.Dto;

namespace Application.Category.Dto;

public class CategoryResponse : BaseCategory
{
	public IReadOnlyCollection<BaseProduct>? Products { get; init; }
}