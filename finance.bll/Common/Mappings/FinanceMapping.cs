using AutoMapper;
using finance.BLL.ModelsDTO;
using finance.DLL.Models;

namespace finance.BLL.Common.Mappings
{
    public class FinanceMapping : Profile
    {
        public FinanceMapping()
        {
            CreateMap<FinancialCategoryDTO, FinancialCategory>();
            CreateMap<CreateFinancialCategoryDTO, FinancialCategory>();
            CreateMap<FinancialCategory, FinancialCategoryDTO>();
            CreateMap<HistoryDTO, History>();
            CreateMap<History, HistoryDTO>()
                .ForMember(dest => dest.WalletFrom, opt => opt.MapFrom(src => src.WalletFromName))
                .ForMember(dest => dest.WalletTo, opt => opt.MapFrom(src => src.WalletToName))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CategoryName));
            CreateMap<WalletDTO, Wallet>();
            CreateMap<CreateWalletDTO, Wallet>();
            CreateMap<Wallet, WalletDTO>();
            CreateMap<TransactionDTO, Transaction>();
            CreateMap<CreateTransactionDTO, Transaction>();
            CreateMap<Transaction, TransactionDTO>();
            CreateMap<TransactionTypeDTO, TransactionType>();
            CreateMap<TransactionType, TransactionTypeDTO>();
        }
    }
}
