using Application.Category.Dto;

namespace Application.Product.Dto;

public class ProductResponse : BaseProduct
{
	public IReadOnlyCollection<BaseCategory>? Categories { get; init; }
}