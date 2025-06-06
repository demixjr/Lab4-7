using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL;
using BLL.dto;

namespace BLL.services
{
    public class AnnouncementService : IAnnouncementService
    {
        IMapper mapper;
        public AnnouncementService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public bool AddAnnouncement(IUnitOfWork unitOfWork, AnnouncementDto announcementDto)
        {

            if (FindAnnouncementByTitle(unitOfWork, announcementDto.Title) != null)
                throw new ValidationException("Оголошення з такою назвою вже існує");

            var announcement = mapper.Map<Announcement>(announcementDto);
        
            var user = unitOfWork.GetRepository<User>().Find(u => u.Username == announcementDto.Username);
            user.Announcements.Add(announcement);
            var category = unitOfWork.GetRepository<Category>().Find(c => c.Name == announcementDto.Category.Name);
            if (category == null)
                throw new EntityNotFoundException("Такої категорії не існує. Оголошення не буде створено");
            category.Announcements.Add(announcement);
            var subcategory = unitOfWork.GetRepository<Subcategory>().Find(s => s.Name == announcementDto.Subcategory.Name);
            if (subcategory == null)
                throw new EntityNotFoundException("Такої підкатегорії не існує. Оголошення не буде створено");
            subcategory.Announcements.Add(announcement);
            List<Tag> tags = new List<Tag>();
            foreach(TagDto tagDto in announcementDto.Tags)
            {
                var tag = unitOfWork.GetRepository<Tag>().Find(t => t.Name == tagDto.Name);
                if (tag == null)
                    throw new EntityNotFoundException("Тег не знайдено, не вдалося зареєструвати оголошення");
                tags.Add(tag);
            }

            announcement.User = user;
            announcement.Category = category;
            announcement.Subcategory = subcategory;
            announcement.Tags = tags;

            foreach (var tag in tags)
            {
                tag.Announcements.Add(announcement);
                unitOfWork.GetRepository<Tag>().Update(tag); 
            }
            announcement.CategoryName = category.Name;
            announcement.SubcategoryName = subcategory.Name;
           
            unitOfWork.GetRepository<Announcement>().Add(announcement);
            unitOfWork.GetRepository<User>().Update(user);
            unitOfWork.GetRepository<Category>().Update(category);
            unitOfWork.GetRepository<Subcategory>().Update(subcategory);
          
            unitOfWork.Save();
            return true;
        }

        public AnnouncementDto FindAnnouncementByTitle(IUnitOfWork unitOfWork, string title)
        {
            var ann = unitOfWork.GetRepository<Announcement>().Find(x => x.Title == title);
            var annDto = mapper.Map<AnnouncementDto>(ann);
            return annDto;
        }
        public List<AnnouncementDto> FindAllAnnouncements(IUnitOfWork unitOfWork)
        {
            var allAnn = unitOfWork.GetRepository<Announcement>().GetAll();
            var annDto = mapper.Map<List<AnnouncementDto>>(allAnn);
            return annDto;
        }
        public bool DeleteAnnouncement(IUnitOfWork unitOfWork, string title, string username)
        {
            var user = unitOfWork.GetRepository<User>().Find(u => u.Username == username);
            var announcement = unitOfWork.GetRepository<Announcement>().Find(a => a.Title == title);
            if (user == null) 
                throw new EntityNotFoundException("Такого користувача не існує");
            if (announcement == null)
                throw new EntityNotFoundException("Такого оголошення не існує");

            if(username ==  announcement.Username)
            {
                List<Tag> tags = announcement.Tags.ToList();
                var subcategory = announcement.Subcategory;

                unitOfWork.GetRepository<Announcement>().Remove(announcement);
                user.Announcements.Remove(announcement);
                foreach(Tag tag in tags)
                {
                    tag.Announcements.Remove(announcement);
                }
                unitOfWork.Save();
                return true;
            }
            else
                return false;
        }
    }
}
