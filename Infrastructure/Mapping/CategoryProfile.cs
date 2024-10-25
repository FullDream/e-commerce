using Application.Dto;
using AutoMapper;
using Core.Entities;

namespace Infrastructure.Mapping;

public class CategoryProfile : Profile
{
	public CategoryProfile()
	{
		CreateMap<Category, CategoryDto>().ForMember(dest => dest.Products,
			opt => opt.MapFrom(src => src.Products.Select(p => p.Id)));
		CreateMap<CreateCategoryDto, Category>();
		CreateMap<UpdateCategoryDto, Category>()
			.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
	}
}