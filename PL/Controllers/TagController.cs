using AutoMapper;
using BLL.dto;
using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PL.request_models;
using PL.response_models;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {

            private IServiceFacade service;
            private IMapper mapper;
            public TagController(IServiceFacade serviceFacade, IMapper mapper)
            {
                service = serviceFacade;
                this.mapper = mapper;
            }
        [HttpGet("tags")]
        [AllowAnonymous]
        public ActionResult<TagResponseModel> GetAllTags()
        {
            try
            {
                var tags = mapper.Map<List<TagResponseModel>>(service.FindAllTags());

                return Ok(tags);
            }
            catch (EntityNotFoundException e)
            {
                return Ok(e.Message);
            }
            catch (Exception)
            {
                return BadRequest("Помилка виводу тегів");
            }

        }

        [HttpGet("find-announcements-by-tag/{tag}")]
        public ActionResult<AnnouncementResponseModel> AnnouncementsByTag(string tag)
        {
            try
            {
                var tagDto = service.FindTag(tag);
                if (tagDto == null)
                    return BadRequest("Такого тега не існує");
                var responseTag = mapper.Map<TagResponseModel>(tagDto);
                var announcements = responseTag.Announcements;
                if (announcements.Count == 0)
                    return Ok("Оголошень за тегом не знайдено");
                return Ok(announcements);
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

        [HttpPost("add")]
            [Authorize]
            public ActionResult<string> AddTag([FromBody] TagRequestModel tag)
            {
                try
                {
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized(new { message = "Потрібна авторизація" });
                }
                service.AddTag(tag.Name);
                    return Ok("Тег додано");
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
}
