using EMaster.Domain.Entities;
using EMaster.Domain.Requests;
using EMaster.Domain.Responses;
using Mapster;

namespace EMaster.Data.Mappings
{
    public static class MapsterConfiguration
    {
        public static void ConfigureMappings()
        {
            TypeAdapterConfig<User, UserResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Email, src => src.Email);

            TypeAdapterConfig<Category, CategoryResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name);

            TypeAdapterConfig<Income, IncomeResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.CategoryID, src => src.CategoryID)
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.Date, src => src.Date)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.CategoryName, src => src.Category.Name);

            TypeAdapterConfig<Expense, ExpenseResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.CategoryID, src => src.CategoryID)
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.Date, src => src.Date)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.CategoryName, src => src.Category.Name);
        }
    }
}
