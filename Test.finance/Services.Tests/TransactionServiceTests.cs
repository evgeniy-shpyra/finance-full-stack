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
    public class TransactionServiceTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IMapper> mapperMock;
        private ITransactionService transactionService;

        [SetUp]
        public void Setup()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            mapperMock = new Mock<IMapper>();
            transactionService = new TransactionService(unitOfWorkMock.Object, mapperMock.Object);
        }

        [Test]
        public void GetAll_ReturnsCorrectTransactionDTOs()
        {
            var transactions = new List<Transaction>
            {
                new Transaction { Id = 1, Price = 100 },
                new Transaction { Id = 2, Price = 200 },
            };

            unitOfWorkMock.Setup(u => u.TransactionsRepository.GetAll()).Returns(transactions);
            mapperMock.Setup(m => m.Map<List<Transaction>, List<TransactionDTO>>(It.IsAny<List<Transaction>>()))
                .Returns((List<Transaction> source) => source.ConvertAll(t => new TransactionDTO { Id = t.Id, Price = t.Price }));

            var result = transactionService.GetAll();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(100, result[0].Price);
            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual(200, result[1].Price);
        }

        [Test]
        public void GetAllTypes_ReturnsCorrectTransactionTypeDTOs()
        {
            var types = new List<TransactionType>
            {
                new TransactionType { Id = 1, Name = "Type1" },
                new TransactionType { Id = 2, Name = "Type2" },
            };

            unitOfWorkMock.Setup(u => u.TransactionTypeRepository.GetAll()).Returns(types);
            mapperMock.Setup(m => m.Map<List<TransactionType>, List<TransactionTypeDTO>>(It.IsAny<List<TransactionType>>()))
                .Returns((List<TransactionType> source) => source.ConvertAll(t => new TransactionTypeDTO { Id = t.Id, Name = t.Name }));

            var result = transactionService.GetAllTypes();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("Type1", result[0].Name);
            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual("Type2", result[1].Name);
        }

        [Test]
        public void Add_ThrowsExceptionForInvalidSendingWalletId()
        {
            var createTransactionDTO = new CreateTransactionDTO { SendingWalletId = 999 };

            unitOfWorkMock.Setup(u => u.WalletsRepository.Get(It.IsAny<int>())).Returns((Wallet)null);

            Assert.Throws<Exception>(() => transactionService.Add(createTransactionDTO));
        }

        [Test]
        public void GetById_ReturnsCorrectTransactionDTO()
        {
            var transactionId = 1;
            var transaction = new Transaction { Id = transactionId, Price = 100 };

            unitOfWorkMock.Setup(u => u.TransactionsRepository.Get(transactionId)).Returns(transaction);
            mapperMock.Setup(m => m.Map<Transaction, TransactionDTO>(It.IsAny<Transaction>()))
                .Returns((Transaction source) => new TransactionDTO { Id = source.Id, Price = source.Price });

            var result = transactionService.GetById(transactionId);

            Assert.AreEqual(transactionId, result.Id);
            Assert.AreEqual(100, result.Price);
        }
    }
}
