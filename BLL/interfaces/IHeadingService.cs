using System.Collections.Generic;
using DAL;
using BLL.dto;

namespace BLL.interfaces
{
    public interface IHeadingService
    {
        bool AddHeading(IUnitOfWork unitOfWork, HeadingDto headingDto);
        HeadingDto FindHeading(IUnitOfWork unitOfWork, string name);
        List<HeadingDto> FindAllHeadings(IUnitOfWork unitOfWork);

    }
}
