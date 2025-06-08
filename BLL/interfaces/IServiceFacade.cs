using System.Collections.Generic;
using BLL.dto;
public interface IServiceFacade
{
    // User methods
    bool AddUser(string username, string password);
    bool ChangeUserPassword(string username, string newPassword);
    List<AnnouncementDto> FindUsersAnnouncements(string username);
    UserDto FindUser(string username);
    bool UserLogin(string username, string password);
    bool DeleteUser(string username);

    // Heading methods
    bool AddHeading(string headingName);
    HeadingDto FindHeading(string name);
    List<HeadingDto> FindAllHeadings();

    // Category methods
    bool AddCategory(string name, string headingName);
    CategoryDto FindCategory(string name);
    List<CategoryDto> FindAllCategories();

    // Subcategory methods
    bool AddSubcategory(string name, string categoryName);
    SubcategoryDto FindSubcategory(string name);
    List<SubcategoryDto> FindAllSubcategories();

    // Tag methods
    bool AddTag(string tagName);
    TagDto FindTag(string tagName);
    List<TagDto> FindAllTags();

    // Announcement methods
    bool AddAnnouncement(string title, string description, string categoryName, string subcategoryName, List<string> tagNames, string username);
    bool ChangeAnnouncement(string title, string description, string categoryName, string subcategoryName, List<string> tagNames, string username);
    AnnouncementDto FindAnnouncement(string name);
    string FindAnnouncementByTag(string tagName);
    List<AnnouncementDto> FindAllAnnouncements();
    bool DeleteAnnouncement(string title, string username);
}
