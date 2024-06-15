using AutoMapper;
using WebApplication1.Domain.Entity;
using WebApplication1.Presentation.Common.DTO_s;
using WebApplication1.Presentation.Common.DTO_s.Order;
using WebApplication1.Presentation.Common.DTO_s.Product;

namespace WebApplication1.Application.Common.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            #region Product
            CreateMap<Product, ProductResponse>()
                .ForMember(x => x.ProductId, src => src.MapFrom(x => x.Id))
                .ForMember(x=>x.Amount,src=>src.MapFrom(x=>x.Amount))
                .ForMember(x => x.Name, src => src.MapFrom(x => x.Name));

            CreateMap<ProductResponse, Product>()
                .ForMember(x => x.Name, src => src.MapFrom(x => x.Name))
                .ForMember(x => x.Amount, src => src.MapFrom(x => x.Amount))
                .ForMember(x => x.Orders, opt => opt.Ignore());

            CreateMap<ProductRequestCreate, Product>()
                .ForMember(x => x.Name, src => src.MapFrom(x => x.Name))
                .ForMember(x => x.Amount, src => src.MapFrom(x => x.Amount));
            #endregion

            #region Order

            CreateMap<Order, OrderRequestCreate>()
                .ForMember(x => x.ProductId, src => src.MapFrom(x => x.Id));

            CreateMap<OrderRequestCreate, Order>()
                .ForMember(x => x.Products, opt => opt.Ignore());


            CreateMap<Order, OrderResponse>()
                .ForMember(x => x.OrderId, src => src.MapFrom(x => x.Id))
                .ForMember(x => x.OrderStatus, src => src.MapFrom(x => x.OrderStatus.ToString()))
                .ForMember(x => x.Products, src => src.MapFrom(x => x.Products));

            CreateMap<Order, OrderRequestUpdate>()
                .ForMember(x => x.OrderId, src => src.MapFrom(x => x.Id))
                .ForMember(x => x.OrderStatus, src => src.MapFrom(x => x.OrderStatus));

            CreateMap<OrderRequestUpdate, Order>();
            #endregion

        }
    }
}
