using finance.DLL;
using finance.DLL.Models;
using finance.DLL.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Test.finance.Repositories.Tests
{
    [TestFixture]
    public class HistoryRepositoryTests
    {
        private Mock<FinanceContext> contextMock;
        private Mock<DbSet<History>> historyDbSetMock;

        private HistoryRepository historyRepository;

        [OneTimeSetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FinanceContext>();
            optionsBuilder.UseInMemoryDatabase("TestDatabase");
            contextMock = new Mock<FinanceContext>(optionsBuilder.Options);
            historyDbSetMock = new Mock<DbSet<History>>();

            contextMock.Setup(x => x.Set<History>()).Returns(historyDbSetMock.Object);

            historyRepository = new HistoryRepository(contextMock.Object);
        }

        [Test]
        public void Add_AddsHistoryToDbSet()
        {
            // Arrange
            var history = new History();

            // Act
            historyRepository.Create(history);

            // Assert
            historyDbSetMock.Verify(m => m.Add(history), Times.Once);
        }

    }
}