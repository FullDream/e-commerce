using Application.Dto;
using Application.Interfaces.Common;
using Core.Entities;

namespace Application.Interfaces.Services;

public interface ICategoryService: IEntityService<Category, CreateProductDto>;