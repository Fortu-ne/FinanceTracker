using AutoMapper;
using FinanceTracker.Data;
using FinanceTracker.Dto;
using System.Transactions;

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
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>();
        }
    }
}
