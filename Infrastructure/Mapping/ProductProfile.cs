using Application.Dto;
using AutoMapper;
using Core.Entities;

namespace Infrastructure.Mapping;

public class ProductProfile : Profile
{
	public ProductProfile()
	{
		CreateMap<Product, ProductDto>().ForMember(dest => dest.Categories,
			opt => opt.MapFrom(src => src.Categories.Select(c => c.Id)));
		CreateMap<CreateProductDto, Product>();
		CreateMap<UpdateProductDto, Product>()
			.ForMember(dest => dest.Categories, opt => opt.Ignore())
			.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
	}
}