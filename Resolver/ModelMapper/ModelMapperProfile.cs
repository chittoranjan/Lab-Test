using AutoMapper;
using Model.DtoModels.ExpenseDtoModels;
using Model.EntityModels.ExpenseModels;

namespace Resolver.ModelMapper
{
    public class ModelMapperProfile : Profile
    {
        public ModelMapperProfile()
        {
            #region Auth Map
            CreateMap<ExpenseItemDto, ExpenseItem>().ReverseMap();

            #endregion
            
        }
    }
}
