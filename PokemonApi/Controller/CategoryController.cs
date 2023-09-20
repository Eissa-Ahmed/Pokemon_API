using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Model;
using Pokemon.Model.IRpo;
using Pokemon.Model.Models;
using Pokemon.Model.ModelsDTO;

namespace PokemonApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Ctor
        public readonly IUnitOfWork services;
        private readonly IMapper mapper;

        public CategoryController(IUnitOfWork services, IMapper mapper)
        {
            this.services = services;
            this.mapper = mapper;
        }
        #endregion

        #region Action

        #region GetPokemonByCategory
        [HttpGet("Pokemon/{id:int}")]
        public IActionResult GetPokemonByCategory(int id)
        {
            try
            {
                if(!services.category.CategoryExists(id))
                    return NotFound();

                var data = mapper.Map<IEnumerable<PokemonDTO>>(services.category.GetPokemonsByCategory(id));
                return Ok(new Response<IEnumerable<PokemonDTO>>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<IEnumerable<PokemonDTO>>
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }
        #endregion

        #region GetAll
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                var data = mapper.Map<IEnumerable<CategoryDTO>>(services.category.GetAll());
                return Ok(new Response<IEnumerable<CategoryDTO>>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<IEnumerable<CategoryDTO>>
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }
        #endregion

        #region GetById
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = mapper.Map<CategoryDTO>(services.category.Get(id));
                if (data is null)
                    return NotFound();

                return Ok(new Response<CategoryDTO>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<CategoryDTO>
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }
        #endregion

        #region Delete
        [HttpDelete()]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                
                if (!services.category.CategoryExists(id))
                    return NotFound();

                var category = services.category.Get(id);
                if (category == null)
                    throw new Exception("Category Is Not Exist");

                services.category.DeleteCategory(id);
                await services.SaveChanges();

                return Ok(new Response<CategoryDTO>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Deleted",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<CategoryDTO>
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }
        #endregion

        #region Create
        [HttpPost()]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryDTO model)
        {
            try
            {
                if (model is null)
                    return NotFound();

                var category = services.category.GetAll().Where(i => i.Name.Trim().ToLower() == model.Name.Trim().ToLower()).FirstOrDefault();
                if (category != null)
                    throw new Exception("Category Is Exist");

                services.category.CreateCategory(mapper.Map<Category>(model));
                await services.SaveChanges();

                return Ok(new Response<CategoryDTO>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<CategoryDTO>
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }
        #endregion

        #region Update
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id ,[FromForm] CategoryDTO model)
        {
            try
            {
                if(!services.category.CategoryExists(id))
                    return NotFound();

                if (model is null)
                    return NotFound();

                services.category.UpdateCategory(id ,mapper.Map<Category>(model));
                await services.SaveChanges();
                return Ok(new Response<CategoryDTO>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Updated",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<CategoryDTO>
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }
        #endregion

        #endregion
    }
}
