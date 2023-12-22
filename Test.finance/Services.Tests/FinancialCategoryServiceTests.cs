using AutoMapper;
using finance.bll.Services;
using finance.BLL.ModelsDTO;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using finance.DLL.UnitOfWork;
using Moq;

namespace Test.finance.Services.Tests
{
    [TestFixture]
    public class FinancialCategoryServiceTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IMapper> mapperMock;
        private IFinancialCategoryService financialCategoryService;

        [SetUp]
        public void Setup()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            mapperMock = new Mock<IMapper>();
            financialCategoryService = new FinancialCategoryService(unitOfWorkMock.Object, mapperMock.Object);
        }

        [Test]
        public void GetAll_ReturnsCorrectFinancialCategoryDTOs()
        {
            var categories = new List<FinancialCategory>
            {
                new FinancialCategory { Id = 1, Name = "Category1" },
                new FinancialCategory { Id = 2, Name = "Category2" },
            };

            unitOfWorkMock.Setup(u => u.FinancialCategoryRepository.GetAll()).Returns(categories);
            mapperMock.Setup(m => m.Map<List<FinancialCategory>, List<FinancialCategoryDTO>>(It.IsAny<List<FinancialCategory>>()))
                .Returns((List<FinancialCategory> source) => source.ConvertAll(c => new FinancialCategoryDTO { Id = c.Id, Name = c.Name }));

            var result = financialCategoryService.GetAll();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("Category1", result[0].Name);
            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual("Category2", result[1].Name);
        }

        [Test]
        public void GetById_ReturnsCorrectFinancialCategoryDTO()
        {
            var categoryId = 1;
            var category = new FinancialCategory { Id = categoryId, Name = "Category1" };

            unitOfWorkMock.Setup(u => u.FinancialCategoryRepository.Get(categoryId)).Returns(category);
            mapperMock.Setup(m => m.Map<FinancialCategory, FinancialCategoryDTO>(It.IsAny<FinancialCategory>()))
                .Returns((FinancialCategory source) => new FinancialCategoryDTO { Id = source.Id, Name = source.Name });

            var result = financialCategoryService.GetById(categoryId);

            Assert.AreEqual(categoryId, result.Id);
            Assert.AreEqual("Category1", result.Name);
        }

        [Test]
        public void Add_ThrowsExceptionForReservedName()
        {
            var createFinancialCategoryDTO = new CreateFinancialCategoryDTO { Name = "Transaction between wallet" };

            Assert.Throws<Exception>(() => financialCategoryService.Add(createFinancialCategoryDTO));
        }

        [Test]
        public void Remove_ThrowsExceptionForReservedFinancialCategory()
        {
            var reservedCategory = new FinancialCategoryDTO { Id = 1, Name = "Transaction between wallet" };

            unitOfWorkMock.Setup(u => u.FinancialCategoryRepository.GetAll()).Returns(new List<FinancialCategory>
            {
                new FinancialCategory { Id = reservedCategory.Id, Name = reservedCategory.Name },
                new FinancialCategory { Id = 2, Name = "NormalCategory" },
            });

            Assert.Throws<Exception>(() => financialCategoryService.Remove(reservedCategory));
        }
    }
}
