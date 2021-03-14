using AutoMapper;
using System;

namespace Eli.Azure.Functions.Test
{
    internal class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(productModel => productModel.Id, mapExpression => mapExpression.MapFrom(product => product.Id))
                .ForMember(productModel => productModel.Name, mapExpression => mapExpression.MapFrom(product => product.Name))
                .ForMember(productModel => productModel.Price, mapExpression => mapExpression.MapFrom(product => Math.Floor(product.Price * 100)));

            CreateMap<ProductModel, Product>()
                .ForMember(product => product.Id, mapExpression => mapExpression.MapFrom(productModel => productModel.Id))
                .ForMember(product => product.Name, mapExpression => mapExpression.MapFrom(productModel => productModel.Name))
                .ForMember(product => product.Price, mapExpression => mapExpression.MapFrom(productModel => productModel.Price == 0 ? 0 : productModel.Price / 100));
        }
    }
}
