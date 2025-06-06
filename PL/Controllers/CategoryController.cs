using AutoMapper;
using BLL.dto;
using BLL.interfaces;
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
    public class CategoryController : ControllerBase
    {
        private IServiceFacade service;
        private IMapper mapper;
        public CategoryController(IServiceFacade serviceFacade, IMapper mapper)
        {
            service = serviceFacade;
            this.mapper = mapper;
        }

        [HttpPost("add-category")]
        [AllowAnonymous]
        //[Authorize]
        public ActionResult<CategoryResponseModel> AddCategory([FromBody] CategoryRequestModel category)
        {
            try
            {
                if (service.AddCategory(category.Name, category.Heading.Name))
                {
                    return Ok("Категорію додано");
                }
                else
                    return BadRequest("Не вдалося додати категорію");
                
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


        [HttpGet("get-all-categories")]
        [AllowAnonymous]
        public ActionResult<CategoryResponseModel> GetAllCategories()
        {
            try
            {
                var categories = mapper.Map<List<CategoryResponseModel>>(service.FindAllCategories());

                return Ok(categories);
            }
            catch (EntityNotFoundException e)
            {
                return Ok(e.Message);
            }
            catch (Exception)
            {
                return BadRequest("Помилка виводу категорій");
            }

        }

    }
}
