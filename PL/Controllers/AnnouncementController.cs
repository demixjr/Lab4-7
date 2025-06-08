using System.Security.Claims;
using AutoMapper;
using BLL;
using BLL.dto;
using BLL.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PL.request_models;
using PL.response_models;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {

        private readonly IServiceFacade facade;
        private readonly IMapper mapper;

        public AnnouncementController(IServiceFacade service, IMapper mapper)
        {
            facade = service;
            this.mapper = mapper;
        }

        [HttpGet("announcements")]

        public ActionResult<AnnouncementResponseModel> GetAllAnnouncements()
        {
            try
            {
                var subcategories = mapper.Map<List<AnnouncementResponseModel>>(facade.FindAllAnnouncements());
                return Ok(subcategories);
            }
            catch (EntityNotFoundException e)
            {
                return Ok(e.Message);
            }
            catch (Exception)
            {
                return BadRequest("Помилка виводу оголошень");
            }

        }
        [HttpGet("my-announcements")]
        [Authorize]
        public ActionResult<AnnouncementResponseModel> MyAnnouncements()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized(new { message = "Потрібна авторизація" });
                }
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                if (username == null)
                    return Forbid("Увійдіть в акаунт");
                var announcements = facade.FindUsersAnnouncements(username);
                var dto = mapper.Map<List<AnnouncementResponseModel>>(announcements);
                return Ok(dto);
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

        [HttpPost("add")]
        [Authorize]
        public ActionResult<string> AddAnnouncement([FromBody] AnnouncementRequestModel requestModel)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized(new { message = "Потрібна авторизація" });
                }
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                if (username == null)
                    return Forbid("Увійдіть в акаунт");

                var tags = requestModel.Tags;
                List<string> strTags = new List<string>();
                foreach (var tag in tags)
                {
                    strTags.Add(tag.Name);
                }
                var dto = mapper.Map<AnnouncementDto>(requestModel);
                facade.AddAnnouncement(requestModel.Title, requestModel.Description, requestModel.Category.Name, requestModel.Subcategory.Name, strTags, username);
                return Ok("Оголошення додано");

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

        [HttpPut("change")]
        [Authorize]
        public ActionResult<string> ChangeAnnouncement([FromBody] AnnouncementRequestModel requestModel)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized(new { message = "Потрібна авторизація" });
                }
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                if (username == null)
                    return Forbid();

                var tags = requestModel.Tags;
                List<string> strTags = new List<string>();
                foreach (var tag in tags)
                {
                    strTags.Add(tag.Name);
                }
                var dto = mapper.Map<AnnouncementDto>(requestModel);
                if (facade.ChangeAnnouncement(requestModel.Title, requestModel.Description, requestModel.Category.Name, requestModel.Subcategory.Name, strTags, username))
                    return Ok("Оголошення успішно змінено");
                else
                    return BadRequest("Не вдалося змінити оголошення");

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

        [HttpDelete("delete/{title}")]
        [Authorize]
        public ActionResult<string> DeleteAnnouncement(string title)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized(new { message = "Потрібна авторизація" });
                }
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                if (username == null)
                    return Forbid();

                var ann = facade.FindAnnouncement(title);
                if (ann.Username == username)
                {
                    facade.DeleteAnnouncement(title, username);
                    return Ok($"Оголошення {title} видалено");
                }
                else
                    return BadRequest("Імя користувача і автора не співпадає");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
