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

            [HttpPost("add-tag")]
        [AllowAnonymous]    
            //[Authorize]
            public ActionResult<TagResponseModel> AddTag([FromBody] TagRequestModel tag)
            {
                try
                {
                service.AddTag(tag.Name);
                    var dto = mapper.Map<TagDto>(tag);
                    var responseModel = mapper.Map<TagResponseModel>(dto);
                    return Ok(responseModel);
                }
                catch (ValidationException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception)
                {
                    return StatusCode(500, "Помилка сервера");
                }
            }


            [HttpGet("get-all-tags")]
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
       
    }
}
