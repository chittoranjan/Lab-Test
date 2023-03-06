using AutoMapper;
using Model.DtoModels.ExpenseDtoModels;
using Model.EntityModels.ExpenseModels;

namespace Resolver.ModelMapper
{
    public class ModelMapperProfile : Profile
    {
        public ModelMapperProfile()
        {
            #region Expense Models Map

            CreateMap<ExpenseItemDto, ExpenseItem>().ReverseMap();
            CreateMap<ExpenseItem, ExpenseItemSearchDto>();

            CreateMap<ExpenseDto, Expense>().ReverseMap();
            CreateMap<ExpenseDetailDto, ExpenseDetail>().ReverseMap();

            #endregion
            
        }
    }
}
