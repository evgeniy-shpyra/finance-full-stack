using AutoMapper;
using finance.BLL.ModelsDTO;
using finance.DLL.Models;

namespace finance.MVC.Models
{
    public class Mapper
    {
        public static IMapper WalletToWalletViewsMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<WalletDTO, WalletViews>().ForMember(dest => dest.WalletId, opt => opt.MapFrom(src => src.Id));
        }).CreateMapper();

        public static IMapper CategoryDTOCategoryViewsMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<FinancialCategoryDTO, FinancialCategoryViews>().ForMember(dest => dest.FinancialCategoryId, opt => opt.MapFrom(src => src.Id));
        }).CreateMapper();

        public static IMapper HistoryToHistoryViewsMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<HistoryDTO, HistoryViews>().ForMember(dest => dest.HistoryId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

        }).CreateMapper();

        public static IMapper TransactionTypeToTransactionTypeViewsMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TransactionTypeDTO, TransactionTypeViews>().ForMember(dest => dest.TransactionTypeViewsId, opt => opt.MapFrom(src => src.Id));
        }).CreateMapper();
    }
}
