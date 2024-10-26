using Application.Category.Dto;
using AutoMapper;
using Core.Entities;

namespace Infrastructure.Mapping;

public class CategoryProfile : Profile
{
	public CategoryProfile()
	{
		CreateMap<Category, CategoryResponse>().ForMember(dest => dest.Products,
			opt => opt.MapFrom(src => src.Products.Select(p => p.Id)));
		CreateMap<CreateCategoryRequest, Category>();
		CreateMap<UpdateCategoryRequest, Category>()
			.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
	}
}