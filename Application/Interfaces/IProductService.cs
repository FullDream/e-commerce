using Core.Entities;

namespace Application.Interfaces;

public interface IProductService
{
	Task<Product> CreateAsync(Product product);
	Task<Product> UpdateAsync(Product product);
	Task<List<Product>> GetAllAsync();
}