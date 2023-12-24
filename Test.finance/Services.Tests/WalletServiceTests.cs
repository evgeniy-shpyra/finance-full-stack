using AutoMapper;
using finance.bll.Services;
using finance.BLL.ModelsDTO;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using finance.DLL.Repository;
using finance.DLL.UnitOfWork;
using Moq;

namespace Test.finance.Services.Tests
{
    [TestFixture]
    public class WalletServiceTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IMapper> mapperMock;
        private IWalletService walletService;

        [SetUp]
        public void Setup()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            mapperMock = new Mock<IMapper>();
            walletService = new WalletService(unitOfWorkMock.Object, mapperMock.Object);
        }

        [Test]
        public void GetAll_ReturnsCorrectWalletDTOs()
        {
            var wallets = new List<Wallet>
            {
                new Wallet { Id = 1, Name = "Wallet1" },
                new Wallet { Id = 2, Name = "Wallet2" },
            };

            unitOfWorkMock.Setup(u => u.WalletsRepository.GetAll()).Returns(wallets.AsQueryable());
            mapperMock.Setup(m => m.Map<List<Wallet>, List<WalletDTO>>(It.IsAny<List<Wallet>>())).Returns(
                (List<Wallet> source) => source.Select(wallet => new WalletDTO { Id = wallet.Id, Name = wallet.Name }).ToList()
            );

            var result = walletService.GetAll();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("Wallet1", result[0].Name);
            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual("Wallet2", result[1].Name);
        }

        [Test]
        public void Add_ThrowsExceptionForReservedName()
        {
            var createWalletDTO = new CreateWalletDTO { Name = "systemReceivingWallet" };

            Assert.Throws<Exception>(() => walletService.Add(createWalletDTO));
        }

        [Test]
        public void Remove_ThrowsExceptionForReservedWallet()
        {
            var reservedWalletId = 1;

            unitOfWorkMock.Setup(u => u.WalletsRepository.GetAll()).Returns(new List<Wallet>
            {
                new Wallet { Id = reservedWalletId, Name = "systemReceivingWallet" },
                new Wallet { Id = 2, Name = "NormalWallet" },
            }.AsQueryable());

            Assert.Throws<Exception>(() => walletService.Remove(reservedWalletId));
        }

        [Test]
        public void Remove_DeletesWalletAndRelatedTransactionsForNonReservedWallet()
        {
            var nonReservedWalletId = 2;
            var relatedTransactionId = 1;

            unitOfWorkMock.Setup(u => u.WalletsRepository.GetAll()).Returns(new List<Wallet>
            {
                new Wallet { Id = 1, Name = "systemReceivingWallet" },
                new Wallet { Id = nonReservedWalletId, Name = "NormalWallet" },
            }.AsQueryable());

            unitOfWorkMock.Setup(u => u.TransactionsRepository.GetAll()).Returns(new List<Transaction>
            {
                new Transaction { Id = relatedTransactionId, ReceivingWalletId = nonReservedWalletId },
            }.AsQueryable());

            walletService.Remove(nonReservedWalletId);

            unitOfWorkMock.Verify(u => u.TransactionsRepository.Delete(relatedTransactionId), Times.Once);
            unitOfWorkMock.Verify(u => u.WalletsRepository.Delete(nonReservedWalletId), Times.Once);
            unitOfWorkMock.Verify(u => u.Save(), Times.Once);
        }
    }
}
