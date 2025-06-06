using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.dto;
using DAL;
using BLL.interfaces;
using Microsoft.EntityFrameworkCore;

namespace BLL.services
{
    public class TagService:ITagService
    {
        IMapper mapper;
        public TagService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public bool AddTag(IUnitOfWork unitOfWork, TagDto tagDto)
        {
            if (FindTagByName(unitOfWork, tagDto.Name) != null)
                throw new ValidationException("Такий тег вже існує");

            var tag = mapper.Map<Tag>(tagDto);
            unitOfWork.GetRepository<Tag>().Add(tag);
            unitOfWork.Save();
            return true;
        }

        public TagDto FindTagByName(IUnitOfWork unitOfWork, string tagName)
        {
            return mapper.Map<TagDto>(unitOfWork.GetRepository<Tag>().Find(c => c.Name == tagName));
        }
        public List<TagDto> FindAllTags(IUnitOfWork unitOfWork)
        {
            return mapper.Map<List<TagDto>>(unitOfWork.GetRepository<Tag>().GetAll().Include(t => t.Announcements));
        }

    }
}
