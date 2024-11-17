using AutoMapper;
using Contracts.Dto.Product;
using Core.Entities;

namespace Contracts.Profiles;

public class ProductProfile : Profile
{
	public ProductProfile()
	{
		CreateMap<Product, ProductResponse>();
		CreateMap<Product, BaseProduct>();
		CreateMap<CreateProductRequest, Product>();
		CreateMap<UpdateProductRequest, Product>()
			.ForMember(dest => dest.Categories, opt => opt.Ignore())
			.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
	}
}