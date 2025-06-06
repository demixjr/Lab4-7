using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.dto;

namespace BLL.interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserDto user);
    }
}
