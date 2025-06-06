using DAL;
using BLL.dto;
using System.Collections.Generic;
using AutoMapper;
using BLL.interfaces;
using Microsoft.EntityFrameworkCore;

namespace BLL.services
{
    public class SubcategoryService: ISubcategoryService
    {
        IMapper mapper;
        public SubcategoryService (IMapper mapper)
        {
            this.mapper = mapper;
        }
        public bool AddSubcategory(IUnitOfWork unitOfWork, SubcategoryDto subcategoryDto)
        {
            if (FindSubcategory(unitOfWork, subcategoryDto.Name) != null)
                throw new ValidationException("Така підкатегорія вже існує");

            var subcategory = mapper.Map<Subcategory>(subcategoryDto);
            var category = unitOfWork.GetRepository<Category>().Find(s => s.Name == subcategoryDto.Category.Name);
            category.Subcategories.Add(subcategory);
            subcategory.Category = category;

            unitOfWork.GetRepository<Category>().Update(category);
            unitOfWork.GetRepository<Subcategory>().Add(subcategory);
            unitOfWork.Save();
            return true;
        }
       
        public SubcategoryDto FindSubcategory(IUnitOfWork unitOfWork, string name)
        {
            return mapper.Map<SubcategoryDto>(unitOfWork.GetRepository<Subcategory>().Find(c => c.Name == name));
        }

        public List<SubcategoryDto> FindAllSubcategories(IUnitOfWork unitOfWork)
        {
            return mapper.Map<List<SubcategoryDto>>(unitOfWork.GetRepository<Subcategory>().GetAll().Include(s => s.Announcements));
        }
    }
}
