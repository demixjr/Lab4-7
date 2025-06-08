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

        [HttpPost("add")]
        [Authorize]
        public ActionResult<string> AddSubcategory([FromBody] SubcategoryRequestModel subcategory)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized(new { message = "Потрібна авторизація" });
                }
                service.AddSubcategory(subcategory.Name, subcategory.CategoryName);
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


        [HttpGet("subcategories")]
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
