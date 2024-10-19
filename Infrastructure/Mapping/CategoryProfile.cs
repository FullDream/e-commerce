using Application.Dto;
using AutoMapper;
using Core.Entities;

namespace Infrastructure.Mapping;

public class CategoryProfile: Profile
{
	public CategoryProfile()
	{
		CreateMap<CreateCategoryDto, Category>();
		CreateMap<UpdateCategoryDto, Category>()
			.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
	}
	
}