using AutoMapper;
using finance.BLL.ModelsDTO;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using finance.DLL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace finance.bll.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public HistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public List<HistoryDTO> GetByCategory(string category)
        {
            var i = unitOfWork.HistoryRepository
                 .GetAll().ToList();

             var history = unitOfWork.HistoryRepository
                 .GetAll()
                 .Where(rec => rec.CategoryName == category)
                 .ToList();

            var historyDTOs = _mapper.Map<List<History>, List<HistoryDTO>>(history);

            return historyDTOs;
        }

        public List<HistoryDTO> GetByTransactionType(int typeId)
        {
            var history = unitOfWork.HistoryRepository
                 .GetAll()
                 .Where(rec => rec.TransactionTypeId == typeId)
                 .ToList();

            var historyDTOs = _mapper.Map<List<History>, List<HistoryDTO>>(history);

            return historyDTOs;
        }

        public List<HistoryDTO> GetAll()
        {
            var history = unitOfWork.HistoryRepository
                .GetAll()
                .ToList();

            var historyDTOs = _mapper.Map<List<History>, List<HistoryDTO>>(history);

            return historyDTOs;
        }

    }
}
