using AutoMapper;
using Contracts.Dto.Category;
using Core.Entities;

namespace Contracts.Profiles;

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