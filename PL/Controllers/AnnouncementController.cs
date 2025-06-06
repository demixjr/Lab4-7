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
        [HttpPost("add")]
        [Authorize]
        public ActionResult<AnnouncementResponseModel> AddAnnouncement([FromBody] AnnouncementRequestModel requestModel)
        {
            try
            {
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
                facade.AddAnnouncement(requestModel.Title, requestModel.Description, requestModel.Category.Name, requestModel.Subcategory.Name, strTags, username);
                return Ok(mapper.Map<AnnouncementResponseModel>(dto));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet("announcements")]
        [AllowAnonymous]
        public ActionResult<AnnouncementResponseModel> GetAllSubcategories()
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
        [HttpDelete]
        [Authorize]
        public ActionResult<AnnouncementResponseModel> DeleteAnnouncement(string title)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                if (username == null)
                    return Forbid();

                var ann = facade.FindAnnouncement(title);
                if (ann.Username == username)
                {
                    facade.DeleteAnnouncement(title, username);
                    return Ok();
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
