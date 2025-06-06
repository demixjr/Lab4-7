using AutoMapper;
using BLL.dto;
using DAL;
using BLL.interfaces;

namespace BLL.services
{
    public class UserService:IUserService
    {
        IMapper mapper;
        public UserService(IMapper mapper) 
        {
            this.mapper = mapper;
        }

        public bool AddUser(IUnitOfWork unitOfWork, UserDto userDto)
        {
            if (FindUserByUsername(unitOfWork, userDto.Username) != null)
                throw new ValidationException("Такий користувач вже існує");

            var user = mapper.Map<User>(userDto);
            unitOfWork.GetRepository<User>().Add(user);
            unitOfWork.Save();
            return true;
        }

        public UserDto FindUserByUsername(IUnitOfWork unitOfWork, string username)
        {
            return mapper.Map<UserDto>(unitOfWork.GetRepository<User>().Find(c => c.Username == username));
        }
        public bool ChangeUserPassword(IUnitOfWork unitOfWork, UserDto userDto, string newPassword)
        {
            var userCheck = FindUserByUsername(unitOfWork, userDto.Username);
            if (userCheck != null)
            {
                var user = unitOfWork.GetRepository<User>().Find(u => u.Username == userDto.Username);
                user.Password = newPassword;
                unitOfWork.GetRepository<User>().Update(user);
                unitOfWork.Save();
                return true;
            }
            else
                return false;
        }
        public bool DeleteUser(IUnitOfWork unitOfWork, UserDto userDto)
        {
            
                var user = unitOfWork.GetRepository<User>().Find(u => u.Username == userDto.Username);
                if (user == null)
                    throw new EntityNotFoundException("Неможливо видалити користувача, оскільки його не знайдено в базі даних");


                unitOfWork.GetRepository<User>().Remove(user);
                unitOfWork.Save();
                return true;
            }

        }
    }

