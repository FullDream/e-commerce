using Application.Product.Dto;
using AutoMapper;
using Core.Entities;

namespace Infrastructure.Mapping;

public class ProductProfile : Profile
{
	public ProductProfile()
	{
		CreateMap<Product, ProductResponse>().ForMember(dest => dest.Categories,
			opt => opt.MapFrom(src => src.Categories.Select(c => c.Id)));
		CreateMap<CreateProductRequest, Product>();
		CreateMap<UpdateProductRequest, Product>()
			.ForMember(dest => dest.Categories, opt => opt.Ignore())
			.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
	}
}