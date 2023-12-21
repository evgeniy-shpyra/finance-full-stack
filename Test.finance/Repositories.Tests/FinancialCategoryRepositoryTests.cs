using finance.DLL;
using finance.DLL.Models;
using finance.DLL.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Test.finance.Repositories.Tests
{
    [TestFixture]
    public class FinancialCategoryRepositoryTests
    {
        private Mock<FinanceContext> contextMock;
        private Mock<DbSet<FinancialCategory>> financialCategoryDbSetMock;

        private FinancialCategoryRepository financialCategoryRepository;

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FinanceContext>();
            optionsBuilder.UseInMemoryDatabase("TestDatabase");
            contextMock = new Mock<FinanceContext>(optionsBuilder.Options);
            financialCategoryDbSetMock = new Mock<DbSet<FinancialCategory>>();

            contextMock.Setup(x => x.Set<FinancialCategory>()).Returns(financialCategoryDbSetMock.Object);

            financialCategoryRepository = new FinancialCategoryRepository(contextMock.Object);
        }

        [Test]
        public void GetById_ReturnsFinancialCategoryById()
        {
            // Arrange
            var categoryId = 1;
            var financialCategory = new FinancialCategory { Id = categoryId };
            financialCategoryDbSetMock.Setup(repo => repo.Find(categoryId)).Returns(financialCategory);

            // Act
            var result = financialCategoryRepository.Get(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<FinancialCategory>(result);
            Assert.AreEqual(categoryId, result.Id);
        }

        [Test]
        public void Add_AddsFinancialCategoryToDbSet()
        {
            // Arrange
            var financialCategory = new FinancialCategory();

            // Act
            financialCategoryRepository.Create(financialCategory);

            // Assert
            financialCategoryDbSetMock.Verify(m => m.Add(financialCategory), Times.Once);
        }

        [Test]
        public void Remove_RemovesFinancialCategoryFromDbSet()
        {
            // Arrange
            var categoryId = 1;
            var financialCategory = new FinancialCategory { Id = categoryId };
            financialCategoryDbSetMock.Setup(repo => repo.Find(categoryId)).Returns(financialCategory);

            // Act
            financialCategoryRepository.Delete(financialCategory.Id);

            // Assert
            financialCategoryDbSetMock.Verify(m => m.Remove(financialCategory), Times.Once);
        }
    }
}