using System.Collections.Generic;
using System;
using BLL.services;
using DAL;
using BLL.dto;
using System.Linq;
using AutoMapper;
using BLL.interfaces;

namespace BLL
{
    public class ServiceFacade : IServiceFacade
    {
        private IUnitOfWork unitOfWork;
        private IValidation validation;
        private IMapper mapper;

        public IUserService userService { get; }
        public ITagService tagService { get; }
        public IHeadingService headingService { get; }
        public ICategoryService categoryService { get; }
        public ISubcategoryService subcategoryService { get; }
        public IAnnouncementService announcementService { get; }

        public ServiceFacade(IUnitOfWork unitOfWork,
            IValidation validation,
            IMapper mapper,
            IUserService userService,
            ITagService tagService,
            IHeadingService headingService,
            ICategoryService categoryService,
            ISubcategoryService subcategoryService,
            IAnnouncementService announcementService)
        {

            this.validation = validation;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.userService = userService;
            this.tagService = tagService;
            this.headingService = headingService;
            this.categoryService = categoryService;
            this.subcategoryService = subcategoryService;
            this.announcementService = announcementService;
        }

        //
        //USER MENU
        //
        public bool AddUser(string username, string password)
        {
            if (validation.IsUsernameValid(username) && validation.IsPasswordValid(password))
            {
                UserDto newUser = new UserDto
                {
                    Username = username,
                    Password = password
                };
                if (userService.AddUser(unitOfWork, newUser))
                    return true;
            }
            return false;
        }

        public bool ChangeUserPassword(string username, string newPassword)
        {
            if (!validation.IsPasswordValid(newPassword))
                return false;
            if (userService.FindUserByUsername(unitOfWork, username) != null)
            {
                UserDto userDto = new UserDto
                {
                    Username = username
                };
                return userService.ChangeUserPassword(unitOfWork, userDto, newPassword);
            }
            else
            {
                return false;
            }
        }
        public List<AnnouncementDto> FindUsersAnnouncements(string username)
        {
            UserDto userDto = userService.FindUserByUsername(unitOfWork, username);
            var announcements = userDto.Announcements;
            return announcements;
        }
        public UserDto FindUser(string username)
        {
            return userService.FindUserByUsername(unitOfWork, username);
        }
        public bool UserLogin(string username, string password)
        {
            UserDto user = FindUser(username);
            if (user != null && user.Password == password)
            {
                return true;
            }
            return false;
        }
        public bool DeleteUser(string username)
        {
            UserDto userDto = new UserDto
            {
                Username = username
            };
            return userService.DeleteUser(unitOfWork, userDto);
        }


        //
        //HEADING
        //
        public bool AddHeading(string headingName)
        {
            if (headingService.FindHeading(unitOfWork, headingName) == null)
            {
                if (validation.IsNameValid(headingName))
                {
                    HeadingDto headingDto = new HeadingDto
                    {
                        Name = headingName
                    };
                    headingService.AddHeading(unitOfWork, headingDto);
                    return true;
                }
                return false;
            }
            throw new ValidationException("Такий заголовок вже існує");
        }

        public HeadingDto FindHeading(string name)
        {
            if (validation.IsNameValid(name))
                return headingService.FindHeading(unitOfWork, name);
            return null;
        }


        public List<HeadingDto> FindAllHeadings()
        {
            return headingService.FindAllHeadings(unitOfWork);
        }

        //
        //CATEGORY
        //
        public bool AddCategory(string name, string headingName)
        {
            if (categoryService.FindCategory(unitOfWork, name) == null)
            {
                if (headingService.FindHeading(unitOfWork, headingName) != null)
                {
                    if (validation.IsNameValid(name))
                    {

                        CategoryDto categoryDto = new CategoryDto
                        {
                            Name = name,
                            Heading = new HeadingDto { Name = headingName }
                        };
                        categoryService.AddCategory(unitOfWork, categoryDto);
                        return true;
                    }
                }
                return false;
            }
            throw new ValidationException("Така категорія вже існує");
        }

        public CategoryDto FindCategory(string name)
        {
            if (validation.IsNameValid(name))
                return categoryService.FindCategory(unitOfWork, name);
            return null;
        }

        public List<CategoryDto> FindAllCategories()
        {
            return categoryService.FindAllCategories(unitOfWork);

        }

        //
        //SUBCATEGORY
        //
        public bool AddSubcategory(string name, string categoryName)
        {
            if (subcategoryService.FindSubcategory(unitOfWork, name) == null)
            {
                if (categoryService.FindCategory(unitOfWork, categoryName) != null)
                {
                    if (validation.IsNameValid(name))
                    {
                        SubcategoryDto subcategoryDto = new SubcategoryDto
                        {
                            Name = name,
                            Category = new CategoryDto { Name = categoryName }
                        };
                        subcategoryService.AddSubcategory(unitOfWork, subcategoryDto);
                        return true;
                    }
                }
                return false;
            }
            throw new ValidationException("Така підкатегорія вже існує");
        }

        public SubcategoryDto FindSubcategory(string name)
        {
            if (validation.IsNameValid(name))
                return subcategoryService.FindSubcategory(unitOfWork, name);
            return null;
        }

        public List<SubcategoryDto> FindAllSubcategories()
        {

            return subcategoryService.FindAllSubcategories(unitOfWork);
            

        }
        //
        //TAG MENU
        //
        public bool AddTag(string tagName)
        {
            if (tagService.FindTagByName(unitOfWork, tagName) == null)
            {
                if (tagName.Count() < 2)
                    throw new ValidationException("Тег занадто короткий");
                TagDto tagDto = new TagDto { Name = tagName };
                return tagService.AddTag(unitOfWork, tagDto);
            }
            throw new ValidationException("Такий тег вже існує");
        }

        public TagDto FindTag(string tagName)
        {
            return tagService.FindTagByName(unitOfWork, tagName);
        }


        public List<TagDto> FindAllTags()
        {
            return tagService.FindAllTags(unitOfWork);
        }

        //
        //ANNOUNCEMENT MENU
        //


        public bool AddAnnouncement(string title, string description, string categoryName, string subcategoryName, List<string> tagNames, string username)
        {
            UserDto user = userService.FindUserByUsername(unitOfWork, username);
            if (user == null)
            {
                throw new EntityNotFoundException("Такого користувача не існує");
            }

  
            List<TagDto> tagList = new List<TagDto>();

            foreach (string tagName in tagNames)
            {
                var tag = FindTag(tagName);
                if (tag == null)
                    throw new EntityNotFoundException("Тег не знайдено");
                tagList.Add(tag);
            }

            List<TagDto> tags = tagList;

            if (announcementService.FindAnnouncementByTitle(unitOfWork, title) != null)
                throw new ValidationException("Уже існує оголошення з такою назвою");

            if (categoryService.FindCategory(unitOfWork, categoryName) == null)
                throw new EntityNotFoundException("Таку категорію не знайдено");

            if (subcategoryService.FindSubcategory(unitOfWork, subcategoryName) == null)
                throw new EntityNotFoundException("Таку підкатегорію не знайдено");

            if (validation.IsNameValid(title) && validation.IsDescriptionValid(description))
            {
                AnnouncementDto announcementDto = new AnnouncementDto
                {
                    Title = title,
                    Description = description,
                    Category = new CategoryDto { Name = categoryName },
                    Username = username,
                    Subcategory = new SubcategoryDto { Name = subcategoryName },
                    Tags = tags 
                };

                announcementService.AddAnnouncement(unitOfWork, announcementDto);
                return true;
            }

            return false;
        }

        public bool ChangeAnnouncement(string title, string description, string categoryName, string subcategoryName, List<string> tagNames, string username)
        {
                UserDto user = userService.FindUserByUsername(unitOfWork, username);
                if (user == null)
                {
                    throw new EntityNotFoundException("Такого користувача не існує");
                }

                List<TagDto> tagList = new List<TagDto>();

                foreach (string tagName in tagNames)
                {
                    var tag = FindTag(tagName);
                    if (tag == null)
                        throw new EntityNotFoundException("Тег не знайдено");
                    tagList.Add(tag);
                }

                List<TagDto> tags = tagList;

                if (announcementService.FindAnnouncementByTitle(unitOfWork, title) != null)
                    throw new ValidationException("Уже існує оголошення з такою назвою");

                if (categoryService.FindCategory(unitOfWork, categoryName) == null)
                    throw new EntityNotFoundException("Таку категорію не знайдено");

                if (subcategoryService.FindSubcategory(unitOfWork, subcategoryName) == null)
                    throw new EntityNotFoundException("Таку підкатегорію не знайдено");

                if (validation.IsNameValid(title) && validation.IsDescriptionValid(description))
                {
                    AnnouncementDto announcementDto = new AnnouncementDto
                    {
                        Title = title,
                        Description = description,
                        Category = new CategoryDto { Name = categoryName },
                        Username = username,
                        Subcategory = new SubcategoryDto { Name = subcategoryName },
                        Tags = tags 
                    };

                    announcementService.ChangeAnnouncement(unitOfWork, announcementDto);
                    return true;
                }
                return false;
            }

        
        public AnnouncementDto FindAnnouncement(string name)
        {
            if (validation.IsNameValid(name))
                return announcementService.FindAnnouncementByTitle(unitOfWork, name);
            return null;
        }

        public string FindAnnouncementByTag(string tagName)
        {
            TagDto tag = tagService.FindTagByName(unitOfWork, tagName);
            if (tag == null)
                return "Тег не знайдено.";
            var announcements = tag.Announcements;
            if (announcements == null)
                return "Оголошень за таким тегом не знайдено";
            string allInfo = "";
            foreach (var a in announcements)
            {
                allInfo += "" + "\n";
            }
            return allInfo;
        }

        public List<AnnouncementDto> FindAllAnnouncements()
        {
             return announcementService.FindAllAnnouncements(unitOfWork);
          
        }

        public bool DeleteAnnouncement(string title, string username)
        {
            var announcement = announcementService.FindAnnouncementByTitle(unitOfWork, title);
            if (announcement == null)
                throw new ValidationException("Такого оголошення не існує");
            if (announcement.Username == username)
            {
                announcementService.DeleteAnnouncement(unitOfWork, title, username);
                return true;
            }
            return false;
        }
        
    }
}
