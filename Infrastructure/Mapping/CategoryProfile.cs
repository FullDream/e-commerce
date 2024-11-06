using Application.Category.Dto;
using AutoMapper;
using Core.Entities;

namespace Infrastructure.Mapping;

public class CategoryProfile : Profile
{
	public CategoryProfile()
	{
		CreateMap<Category, CategoryResponse>();
		CreateMap<Category, BaseCategory>();
		CreateMap<CreateCategoryRequest, Category>();
		CreateMap<UpdateCategoryRequest, Category>()
			.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
	}
}