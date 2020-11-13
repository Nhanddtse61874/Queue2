using AutoMapper;
using Persistence.Models;
using RecommenceSystemCapstoneV2.ViewModels;

namespace RecommenceSystemCapstoneV2.Controllers
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration MapperConfig()
        {
            return new MapperConfiguration(cf =>
            {
                cf.CreateMap<Category, CategoryViewModel>().ReverseMap();
                cf.CreateMap<Category, CreateCategoryViewModel>().ReverseMap();
                cf.CreateMap<ProductRecommencePrice, ProductViewModel>().ReverseMap();
                cf.CreateMap<Product, ProductViewModel>().ReverseMap();
                cf.CreateMap<Product, CreateProductViewModel>().ReverseMap();
                cf.CreateMap<RecommenceHobby, RecommenceByHobbyViewModel>()
                    .ForMember(x => x.ProductRecommenceHobbies, src => src.MapFrom(dest => dest.ProductRecommenceHobbies))
                    .ReverseMap();
                cf.CreateMap<RecommenceHobby, CreateRecommenceByHobbyViewModel>().ReverseMap();


                cf.CreateMap<RecommencePrice, RecommenceByPriceViewModel>()
                 .ForMember(x => x.ProductRecommencePrices, src => src.MapFrom(dest => dest.ProductRecommencePrices))
                 .ReverseMap();
                cf.CreateMap<RecommencePrice, CreateRecommenceByPriceViewModel>().ReverseMap();
                cf.CreateMap<Recommence, RecommenceViewModel>().ReverseMap();
                cf.CreateMap<RecommenceByBoth, RecommenceByBothViewModel>().ReverseMap();

                cf.CreateMap<User, UserViewModel>().ReverseMap();
                cf.CreateMap<User, CreateUserViewModel>().ReverseMap();
                cf.CreateMap<RecommenceHobby, ProductRecommenceHobby>()
                   .ForMember(x => x.RecommenceHobbyId, src => src.MapFrom(dest => dest.Id)).ReverseMap();
                cf.CreateMap<Product, ProductRecommenceHobby>()
                   .ForMember(x => x.Id, src => src.Ignore())
                   .ForMember(x => x.Product, src => src.MapFrom(dest => dest));


                cf.CreateMap<Product, ProductRecommencePrice>()
                  .ForMember(x => x.Id, src => src.Ignore())
                  .ForMember(x => x.Product, src => src.MapFrom(dest => dest));

                cf.CreateMap<ProductViewModel, ProductRecommenceHobby>()
                    .ForMember(x => x.Id, src => src.Ignore())
                    .ForMember(x => x.ProductId, src => src.MapFrom(dest => dest.Id))
                   .ForMember(x => x.Product, src => src.MapFrom(dest => dest)).ReverseMap();

                cf.CreateMap<RecommencePrice, ProductRecommencePrice>()
                 .ForMember(x => x.RecommencePriceId, src => src.MapFrom(dest => dest.Id)).ReverseMap();

                cf.CreateMap<ProductViewModel, ProductRecommencePrice>()
                    .ForMember(x => x.Id, src => src.Ignore())
                    .ForMember(x => x.ProductId, src => src.MapFrom(dest => dest.Id))
                    .ReverseMap();
            });
        }
    }
}
