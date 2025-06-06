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

        [HttpPost("add-heading")]
        [AllowAnonymous]
       // [Authorize]
        public ActionResult<HeadingResponseModel> AddHeading([FromBody] HeadingRequestModel heading)
        {
            try
            {  
                service.AddHeading(heading.Name);
                var headingDto = mapper.Map<HeadingDto>(heading);
                var responseModel = mapper.Map<HeadingResponseModel>(headingDto);
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


        [HttpGet("get-all-headings")]
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
