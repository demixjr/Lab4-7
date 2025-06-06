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
    public class SubcategoryController : ControllerBase
    {
        private IServiceFacade service;
        private IMapper mapper;
        public SubcategoryController(IServiceFacade serviceFacade, IMapper mapper)
        {
            service = serviceFacade;
            this.mapper = mapper;
        }

        [HttpPost("add-subcategory")]
        [AllowAnonymous]
        //[Authorize]
        public ActionResult<SubcategoryResponseModel> AddSubcategory([FromBody] SubcategoryRequestModel subcategory)
        {
            try
            {
                service.AddSubcategory(subcategory.Name, subcategory.CategoryName);
                var dto = mapper.Map<SubcategoryDto>(subcategory);
                var responseModel = mapper.Map<SubcategoryResponseModel>(dto);
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


        [HttpGet("get-all-subcategories")]
        [AllowAnonymous]
        public ActionResult<SubcategoryResponseModel> GetAllSubcategories()
        {
            try
            {
                var subcategories = mapper.Map<List<SubcategoryResponseModel>>(service.FindAllSubcategories());

                return Ok(subcategories);
            }
            catch (EntityNotFoundException e)
            {
                return Ok(e.Message);
            }
            catch (Exception)
            {
                return BadRequest("Помилка виводу підкатегорій");
            }

        }
    }
    }
