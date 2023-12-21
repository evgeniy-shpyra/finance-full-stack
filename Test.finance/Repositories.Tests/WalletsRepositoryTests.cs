using finance.DLL;
using finance.DLL.Models;
using finance.DLL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Moq;

namespace Test.finance.Repositories.Tests
{
    [TestFixture]
    public class WalletsRepositoryTests
    {
        private Mock<FinanceContext> contextMock;
        private Mock<DbSet<Wallet>> walletsDbSetMock;

        private WalletsRepository walletsRepository;
        private List<Wallet> wallets;

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FinanceContext>();
            optionsBuilder.UseInMemoryDatabase("TestDatabase");
            contextMock = new Mock<FinanceContext>(optionsBuilder.Options);
            walletsDbSetMock = new Mock<DbSet<Wallet>>();

            contextMock.Setup(x => x.Set<Wallet>()).Returns(walletsDbSetMock.Object);

       
            var wallets = new List<Wallet> { new Wallet(), new Wallet() };
            contextMock.Setup(x => x.Set<Wallet>()).Returns(walletsDbSetMock.Object);
            walletsDbSetMock.As<IQueryable<Wallet>>().Setup(m => m.Provider).Returns(wallets.AsQueryable().Provider);
            walletsDbSetMock.As<IQueryable<Wallet>>().Setup(m => m.Expression).Returns(wallets.AsQueryable().Expression);
            walletsDbSetMock.As<IQueryable<Wallet>>().Setup(m => m.ElementType).Returns(wallets.AsQueryable().ElementType);
            walletsDbSetMock.As<IQueryable<Wallet>>().Setup(m => m.GetEnumerator()).Returns(wallets.AsQueryable().GetEnumerator());

            walletsRepository = new WalletsRepository(contextMock.Object);

        }

        [Test]
        public void Add_AddsWalletToDbSet()
        {
            // Arrange
            var wallet = new Wallet();

            // Act
            walletsRepository.Create(wallet);

            // Assert
            walletsDbSetMock.Verify(m => m.Add(wallet), Times.Once);
        }

        [Test]
        public void Remove_RemovesWalletFromDbSet()
        {
            // Arrange
            var walletId = 1;
            var wallet = new Wallet { Id = walletId };
            walletsDbSetMock.Setup(m => m.Find(walletId)).Returns(wallet);


            // Act
            walletsRepository.Delete(wallet.Id);

            // Assert
            walletsDbSetMock.Verify(m => m.Remove(wallet), Times.Once);
        }
    }
}