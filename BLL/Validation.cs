using System.Xml.Linq;
using BLL.interfaces;

namespace BLL
{
    public class Validation:IValidation
    {
        public Validation() { }

        public bool IsUsernameValid(string Username)
        {
            if (Username == null)
                throw new ValidationException("Імя не може бути порожнім");
            if(Username.Length < 3)
                throw new ValidationException("Дуже коротке імя");
            if (Username.Length > 32)
                throw new ValidationException("Дуже довге імя");
            return true;
        }
        public bool IsPasswordValid(string Password)
        {
            if (Password == null)
                throw new ValidationException("Пароль не може бути порожнім");
            if (Password.Length < 8)
                throw new ValidationException("Пароль має бути більше 8 символів");
            if (Password.Length > 32)
                throw new ValidationException("Пароль має бути менше 64 символів");
            return true;
        }

        public bool IsNameValid(string name)
        {
            if (name == null)
                throw new ValidationException("Назва не може бути порожньою");
            if (name.Length < 3)
                throw new ValidationException("Дуже коротка назва");
            if (name.Length > 32)
                throw new ValidationException("Дуже довга назва");
            return true;
        }

        public bool IsDescriptionValid(string description)
        {
            if (description.Length > 512)
                throw new ValidationException("Дуже довга назва");
            return true;
        }
     
    }
}
