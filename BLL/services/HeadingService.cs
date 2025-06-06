using System.Collections.Generic;
using BLL.dto;
using AutoMapper;
using BLL.interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace BLL.services
{
    public class HeadingService: IHeadingService
    {
        IMapper mapper;
        public HeadingService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public bool AddHeading(IUnitOfWork unitOfWork, HeadingDto headingDto)
        {
            if (FindHeading(unitOfWork, headingDto.Name) != null)
                throw new ValidationException("Така рубрика вже існує");

            var heading = mapper.Map<Heading>(headingDto);
            unitOfWork.GetRepository<Heading>().Add(heading);
            unitOfWork.Save();
            return true;
        }

        public HeadingDto FindHeading(IUnitOfWork unitOfWork, string name)
        {
            return mapper.Map<HeadingDto>(unitOfWork.GetRepository<Heading>().Find(h => h.Name == name));
        }

        public List<HeadingDto> FindAllHeadings(IUnitOfWork unitOfWork)
        {
            return mapper.Map<List<HeadingDto>>(unitOfWork.GetRepository<Heading>().GetAll().Include(h => h.Categories));
        }

    }
}
