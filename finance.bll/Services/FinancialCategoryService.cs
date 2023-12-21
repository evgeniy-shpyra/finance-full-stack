using AutoMapper;
using finance.BLL.ModelsDTO;
using finance.BLL.Services.Interfaces;
using finance.DLL.Models;
using finance.DLL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace finance.bll.Services
{

    public class FinancialCategoryService : IFinancialCategoryService
    {
        private List<string> reservedNames = new List<string> { "Transaction between wallet" };
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public FinancialCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<FinancialCategoryDTO> GetAll()
        {
            var categories = unitOfWork.FinancialCategoryRepository
                .GetAll()
                .ToList();

            var categoriesDTOs = _mapper.Map<List<FinancialCategory>, List<FinancialCategoryDTO>>(categories);
            return categoriesDTOs;
        }

        public FinancialCategoryDTO GetById(int categoryId)
        {
            var category = unitOfWork.FinancialCategoryRepository.Get(categoryId);

            var categoryDto = _mapper.Map<FinancialCategory, FinancialCategoryDTO>(category);

            return categoryDto;
        }


        public void Add(CreateFinancialCategoryDTO financialCategory)
        {
            if (reservedNames.Contains(financialCategory.Name))
            {
                throw new Exception("You can't create financial category with this name");
            }

            var newCategory = _mapper.Map<CreateFinancialCategoryDTO, FinancialCategory>(financialCategory);

            unitOfWork.FinancialCategoryRepository.Create(newCategory);
            unitOfWork.Save();
        }

        public void Remove(FinancialCategoryDTO financialCategory)
        {

            var reservedId = unitOfWork.FinancialCategoryRepository
               .GetAll()
               .ToList().Find(c => reservedNames.Contains(c.Name)).Id;

            if(reservedId == financialCategory.Id)
            {
                throw new Exception("You can't delete this financial category");
            }

            var category = _mapper.Map<FinancialCategoryDTO, FinancialCategory>(financialCategory);

            unitOfWork.FinancialCategoryRepository.Delete(category.Id);
            unitOfWork.Save();
        }
    }

}
    