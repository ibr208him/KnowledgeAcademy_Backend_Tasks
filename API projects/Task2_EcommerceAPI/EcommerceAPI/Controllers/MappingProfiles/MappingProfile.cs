
using AutoMapper;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;

namespace Ecommerce.API.Controllers.MappingProfiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile() { // constructor

            CreateMap<Products, ProductDTO>()
          .ForMember(productDto => productDto.Category_Name,
           products => products.MapFrom(x => x.Category != null ? x.Category.Name : null));

            CreateMap<ProductPostDTO,Products>();
            CreateMap<Orders, OrderDTO>();
        }


      
    }
}
