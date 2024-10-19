using Application.Dto;
using AutoMapper;
using Core.Entities;

namespace Infrastructure.Mapping;

public class ProductProfile: Profile
{
	public ProductProfile()
	{
		CreateMap<CreateProductDto, Product>();
		CreateMap<UpdateProductDto, Product>()
			.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
	}
}