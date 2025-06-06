using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.interfaces;
using DAL;
using BLL.dto;
using Microsoft.EntityFrameworkCore;


namespace BLL.services
{
    public class CategoryService: ICategoryService
    {
        IMapper mapper;
        public CategoryService(IMapper mapper) 
        {
            this.mapper = mapper;
        }
        public bool AddCategory(IUnitOfWork unitOfWork, CategoryDto categoryDto)
        {
            try
            {
                unitOfWork.BeginTransaction();
                var category = mapper.Map<Category>(categoryDto);
                var heading = unitOfWork.GetRepository<Heading>().GetAll().FirstOrDefault(h => h.Name == categoryDto.Heading.Name);
                if (heading == null)
                    throw new EntityNotFoundException("Такої рубрики не знайдено");

                category.HeadingId = heading.HeadingId;
                category.Heading = heading;

                heading.Categories.Add(category);

                unitOfWork.GetRepository<Category>().Add(category);
                unitOfWork.GetRepository<Heading>().Update(heading);
                unitOfWork.Save();
                unitOfWork.Commit();
                return true;
            }
            catch (EntityNotFoundException e)
            {
                unitOfWork.Rollback();
                throw new EntityNotFoundException(e.Message);   
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                throw;
            }
        }
        public CategoryDto FindCategory(IUnitOfWork unitOfWork, string name)
        {
            return mapper.Map<CategoryDto>(unitOfWork.GetRepository<Category>().Find(c => c.Name == name));
        }

        public List<CategoryDto> FindAllCategories(IUnitOfWork unitOfWork)
        {
            var mapped = mapper.Map<List<CategoryDto>>(unitOfWork.GetRepository<Category>().GetAll().Include(c => c.Subcategories));
            return mapped;
        }
    }
}
