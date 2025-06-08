using Microsoft.AspNetCore.Mvc;
using PL.request_models;
using PL.response_models;
using BLL;
using BLL.interfaces;
using BLL.services;
using BLL.dto;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Authentication;
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IServiceFacade facade;
    private readonly ITokenService tokenService;
    private readonly IMapper mapper;
    
    public UserController(IServiceFacade service, IMapper mapper, ITokenService tokenService)
    {
        facade = service;
        this.tokenService = tokenService;
        this.mapper = mapper;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public ActionResult<string> Register([FromBody] UserRequestModel requestModel)
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return Forbid("Ви вже увійшли в акаунт");
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var dto = mapper.Map<UserDto>(requestModel);
            facade.AddUser(dto.Username, dto.Password);
            return Ok("Користувача додано");
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (EntityNotFoundException enf)
        {
            return BadRequest(new { message = enf.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = e.Message });
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public ActionResult<UserResponseModel> Login([FromBody] UserRequestModel requestModel)
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return Forbid("Ви вже знаходитесь в акаунті");
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            if (facade.UserLogin(requestModel.Username, requestModel.Password))
            {
                var dto = mapper.Map<UserDto>(requestModel);
                var token = tokenService.GenerateToken(dto);

                return Ok(new LoginResponseModel { Token = token, Username = dto.Username });
            }
            else
                return BadRequest("Невірні дані");
        }
        catch (AuthenticationException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (EntityNotFoundException enf)
        {
            return BadRequest(new { message = enf.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = e.Message });
        }
    }

    [HttpPut("change-password")]
    [Authorize]
    public ActionResult<string> ChangePassword([FromBody] ChangePasswordRequestModel requestModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var usernameFromToken = User.Identity.Name;
            if (usernameFromToken != requestModel.Username)
                return Forbid();

            var result = facade.ChangeUserPassword(requestModel.Username, requestModel.NewPassword);
            if (!result)
                return BadRequest(new { message = "Не вдалося змінити пароль." });

            return Ok(new { message = "Пароль успішно змінено." });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (EntityNotFoundException enf)
        {
            return BadRequest(new { message = enf.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = e.Message});
        }
    }

    [HttpDelete("delete/{username}")]
    [Authorize]
    public IActionResult DeleteUser(string username)
    {
        try
        {
            var usernameFromToken = User.Identity.Name;
            if (usernameFromToken != username)
                return Forbid("Спочатку увійдіть в акаунт");

            var deleted = facade.DeleteUser(username);
            if (!deleted)
                return BadRequest(new { message = "Не вдалося видалити користувача." });

            return Ok(new { message = "Користувача видалено." });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (EntityNotFoundException enf)
        {
            return BadRequest(new { message = enf.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = e.Message });
        }
    }
}