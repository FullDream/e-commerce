using Application.Dto;
using Application.Interfaces.Common;

namespace Application.Interfaces.Services;

public interface IProductService : IEntityService<ProductDto, CreateProductDto, UpdateProductDto>;