using AutoMapper;
using finance.BLL.ModelsDTO;


namespace finance.WebAPI.DTO
{
    public class Mapper
    {
        public static IMapper CreateWalletViewsCreateWallet = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateWalletViewsDTO, CreateWalletDTO>();
        }).CreateMapper();

        public static IMapper CreateCategoryViewsCreateCategory = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateFinancialCategoryViewsDTO, CreateFinancialCategoryDTO>();
        }).CreateMapper();
        public static IMapper CreateTransactionViewsCreateTransaction = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateTransactionViewsDTO, CreateTransactionDTO>();
        }).CreateMapper();


        public static IMapper CategoryDTOCategoryViews = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<FinancialCategoryDTO, FinancialCategoryViewsDTO>();
        }).CreateMapper();

        public static IMapper HistoryDTOHistoryViews = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<HistoryDTO, HistoryViewsDTO>();
        }).CreateMapper();

        public static IMapper TransactionDTOTransactionViews = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TransactionDTO, TransactionViewsDTO>();
            cfg.CreateMap<WalletDTO, WalletViewsDTO>();
            cfg.CreateMap<FinancialCategoryDTO, FinancialCategoryViewsDTO>();
            cfg.CreateMap<TransactionTypeDTO, TransactionTypeViewsDTO>();
        }).CreateMapper();
        public static IMapper TransactionTypeDTOTransactionTypeViews = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TransactionTypeDTO, TransactionTypeViewsDTO>();
        }).CreateMapper();
        public static IMapper WalletDTOWalletViews = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<WalletDTO, WalletViewsDTO>();
        }).CreateMapper();
    }
}
