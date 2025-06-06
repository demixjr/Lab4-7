using System;
using System.Collections.Generic;
using BLL.dto;
using DAL;

namespace BLL.interfaces
{
    public interface ICategoryService
    {
        bool AddCategory(IUnitOfWork unitOfWork, CategoryDto categoryDto);
        CategoryDto FindCategory(IUnitOfWork unitOfWork, string name);
        List<CategoryDto> FindAllCategories(IUnitOfWork unitOfWork);
    }
}
