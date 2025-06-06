using BLL.dto;
using DAL;

namespace BLL.interfaces
{
    public interface IUserService
    {
        bool AddUser(IUnitOfWork unitOfWork, UserDto userDto);
        UserDto FindUserByUsername(IUnitOfWork unitOfWork, string username);
        bool ChangeUserPassword(IUnitOfWork unitOfWork, UserDto userDto, string newPassword);
        bool DeleteUser(IUnitOfWork unitOfWork, UserDto userDto);
    }
}
