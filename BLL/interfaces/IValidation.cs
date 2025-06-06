using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.interfaces
{
    public interface IValidation
    {
        bool IsUsernameValid(string Username);
        bool IsPasswordValid(string Password);
        bool IsNameValid(string name);
        bool IsDescriptionValid(string description);
    }
}
