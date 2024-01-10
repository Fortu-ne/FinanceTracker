using AutoMapper;
using FinanceTracker.Data;
using FinanceTracker.Dto;

namespace FinanceTracker.Mapping
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Budget, BudgetDto>();
            CreateMap<BudgetDto, Budget>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
            CreateMap<Transaction, TransactionDto>();   
            CreateMap<TransactionDto, Transaction>();
        }
    }
}
