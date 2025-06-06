using System.Collections.Generic;
using DAL;
using BLL.dto;

namespace BLL.services
{
    public interface IAnnouncementService
    {
        bool AddAnnouncement(IUnitOfWork unitOfWork, AnnouncementDto announcementDto);
        AnnouncementDto FindAnnouncementByTitle(IUnitOfWork unitOfWork, string title);
        List<AnnouncementDto> FindAllAnnouncements(IUnitOfWork unitOfWork);
        bool DeleteAnnouncement(IUnitOfWork unitOfWork, string title, string username);
    }
}
