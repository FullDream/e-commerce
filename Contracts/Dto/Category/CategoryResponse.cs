using Contracts.Dto.Product;

namespace Contracts.Dto.Category;

public class CategoryResponse : BaseCategory
{
	public IReadOnlyCollection<BaseProduct>? Products { get; init; }
}