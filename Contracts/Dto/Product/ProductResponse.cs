using Contracts.Dto.Category;

namespace Contracts.Dto.Product;

public class ProductResponse : BaseProduct
{
	public IReadOnlyCollection<BaseCategory>? Categories { get; init; }
}