using AutoMapper;
using finance.BLL.ModelsDTO;
using finance.DLL.Models;

namespace Test.finance.Services.Tests
{
    public class Mapper
    {

        public static IMapper HistoryHistoryDTO = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<History, HistoryDTO>();
        }).CreateMapper();

        public static IMapper CreateWalletDTOWallet = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateWalletDTO, Wallet>();
        }).CreateMapper();

    }
}
