using System;
using System.Collections.Generic;
using BLL.dto;
using DAL;

namespace BLL.interfaces
{
    public interface ITagService
    {
        bool AddTag(IUnitOfWork unitOfWork, TagDto tagDto);
        TagDto FindTagByName(IUnitOfWork unitOfWork, string tagName);
        List<TagDto> FindAllTags(IUnitOfWork unitOfWork);
    }
}
