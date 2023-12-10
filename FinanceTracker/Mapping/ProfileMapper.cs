using AutoMapper;
using FinanceTracker.Data;
using FinanceTracker.Dto;

namespace FinanceTracker.Mapping
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<BudgetDto, Budget>();
            CreateMap<Budget, BudgetDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
