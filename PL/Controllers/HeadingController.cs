using AutoMapper;
using BLL;
using BLL.dto;
using BLL.interfaces;
using BLL.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PL.request_models;
using PL.response_models;


namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadingController : ControllerBase
    {
        private IServiceFacade service;
        private IMapper mapper;
        private readonly ITokenService tokenService;
        public HeadingController(IServiceFacade serviceFacade, IMapper mapper, ITokenService tokenService)
        {
            service = serviceFacade;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }

        [HttpPost("add")]
        [Authorize]
        public ActionResult<string> AddHeading([FromBody] HeadingRequestModel heading)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized(new { message = "Потрібна авторизація" });
                }
                service.AddHeading(heading.Name);
                return Ok("Заголовок додано");
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


        [HttpGet("headings")]
        [AllowAnonymous]
        public ActionResult<HeadingResponseModel> GetAllHeadings()
        {
            try
            {
                var headings = mapper.Map<List<HeadingResponseModel>>(service.FindAllHeadings());
                return Ok(headings);
            }
            catch(EntityNotFoundException e)
            {
                return Ok(e.Message);
            }
            catch(Exception)
            {
                return BadRequest("Помилка виводу рубрик");
            }

        }

        }
}
