namespace PL.response_models
{
    public class UserResponseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public List<AnnouncementResponseModel> Announcements { get; set; } = new List<AnnouncementResponseModel>();
    }
}
