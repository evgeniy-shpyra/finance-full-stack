using finance.DLL;
using finance.DLL.Models;
using finance.DLL.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Test.finance.Repositories.Tests
{
    [TestFixture]
    public class TransactionsRepositoryTests
    {
        private Mock<FinanceContext> contextMock;
        private Mock<DbSet<Transaction>> transactionsDbSetMock;
        private Mock<DbSet<TransactionType>> transactionTypesDbSetMock;

        private TransactionsRepository transactionsRepository;

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FinanceContext>();
            optionsBuilder.UseInMemoryDatabase("TestDatabase");
            contextMock = new Mock<FinanceContext>(optionsBuilder.Options);
            transactionsDbSetMock = new Mock<DbSet<Transaction>>();
            transactionTypesDbSetMock = new Mock<DbSet<TransactionType>>();

            contextMock.Setup(x => x.Set<Transaction>()).Returns(transactionsDbSetMock.Object);
            contextMock.Setup(x => x.Set<TransactionType>()).Returns(transactionTypesDbSetMock.Object);

            transactionsRepository = new TransactionsRepository(contextMock.Object);
        }

        [Test]
        public void Add_AddsTransactionToDbSet()
        {
            // Arrange
            var transaction = new Transaction();

            // Act
            transactionsRepository.Create(transaction);

            // Assert
            transactionsDbSetMock.Verify(m => m.Add(transaction), Times.Once);
        }

        [Test]
        public void GetById_ReturnsTransactionById()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new Transaction { Id = transactionId };
            transactionsDbSetMock.Setup(m => m.Find(transactionId)).Returns(transaction);

            // Act
            var result = transactionsRepository.Get(transactionId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Transaction>(result);
            Assert.AreEqual(transactionId, result.Id);
        }
    }
}