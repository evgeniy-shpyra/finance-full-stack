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
    public class HistoryServiceTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IMapper> mapperMock;
        private IHistoryService historyService;

        [SetUp]
        public void Setup()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            mapperMock = new Mock<IMapper>();
            historyService = new HistoryService(unitOfWorkMock.Object, mapperMock.Object);
        }

        [Test]
        public void GetByCategory_ReturnsCorrectHistoryDTOs()
        {
            var category = "TestCategory";
            var histories = new List<History>
            {
                new History { Id = 1, CategoryName = category },
                new History { Id = 2, CategoryName = category },
            };

            unitOfWorkMock.Setup(u => u.HistoryRepository.GetAll()).Returns(histories);
            mapperMock.Setup(m => m.Map<List<History>, List<HistoryDTO>>(It.IsAny<List<History>>()))
                .Returns((List<History> source) => source.ConvertAll(h => new HistoryDTO { Id = h.Id, Category = h.CategoryName }));

            var result = historyService.GetByCategory(category);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(category, result[0].Category);
            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual(category, result[1].Category);
        }

        [Test]
        public void GetByTransactionType_ReturnsCorrectHistoryDTOs()
        {
            var typeId = 1;
            var histories = new List<History>
            {
                new History { Id = 1, TransactionTypeId = typeId },
                new History { Id = 2, TransactionTypeId = typeId },
            };

            unitOfWorkMock.Setup(u => u.HistoryRepository.GetAll()).Returns(histories);
            mapperMock.Setup(m => m.Map<List<History>, List<HistoryDTO>>(It.IsAny<List<History>>()))
                .Returns((List<History> source) => source.ConvertAll(h => new HistoryDTO { Id = h.Id }));

            var result = historyService.GetByTransactionType(typeId);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);
        }

        [Test]
        public void GetAll_ReturnsCorrectHistoryDTOs()
        {
            var histories = new List<History>
            {
                new History { Id = 1 },
                new History { Id = 2 },
            };

            unitOfWorkMock.Setup(u => u.HistoryRepository.GetAll()).Returns(histories);
            mapperMock.Setup(m => m.Map<List<History>, List<HistoryDTO>>(It.IsAny<List<History>>()))
                .Returns((List<History> source) => source.ConvertAll(h => new HistoryDTO { Id = h.Id }));

            var result = historyService.GetAll();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);
        }
    }
}
