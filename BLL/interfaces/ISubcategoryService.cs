using System.Collections.Generic;
using BLL.dto;
using DAL;

namespace BLL.interfaces
{
    public interface ISubcategoryService
    {
        bool AddSubcategory(IUnitOfWork unitOfWork, SubcategoryDto subcategoryDto);
        SubcategoryDto FindSubcategory(IUnitOfWork unitOfWork, string name);
        List<SubcategoryDto> FindAllSubcategories(IUnitOfWork unitOfWork);
    }
}
