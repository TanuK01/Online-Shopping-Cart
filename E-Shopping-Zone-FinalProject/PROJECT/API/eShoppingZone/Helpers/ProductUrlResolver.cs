using AutoMapper;
using Core.Entities;
using eShoppingZoneAPI.DTO;

namespace eShoppingZoneAPI.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, DTO.ProductToReturnDTO, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureURL))
            {
                return _config["ApiUrl"]  + source.PictureURL;
            //    return source.PictureURL;
            }

            return null;
        }
    }
}
