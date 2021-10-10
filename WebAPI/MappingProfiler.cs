using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System.Linq;

namespace WebAPI
{
    public class MappingProfiler : Profile
    {
        public MappingProfiler()
        {
            // List Dtos
            CreateMap<Product, ProductDto>();
            CreateMap<Category, CategoryDto>();

            // Update Dtos
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
