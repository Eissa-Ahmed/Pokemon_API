using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Model;
using Pokemon.Model.IRpo;
using Pokemon.Model.Models;
using Pokemon.Model.ModelsDTO;

namespace categoryApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Ctor
        public readonly IUnitOfWork services;
        private readonly IMapper mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.services = unitOfWork;
            this.mapper = mapper;
        }
        #endregion

        #region Action



        #region GetAllAsync
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var res = await services.category.GetAllAsync();
                var items = mapper.Map<IEnumerable<CategoryDTO>>(res);
                var data = new Response<IEnumerable<CategoryDTO>>()
                {
                    Code = 200,
                    Message = "OK",
                    Status = true,
                    Data = items
                };
                return Ok(data);
            }
            catch (Exception ex)
            {

                var data = new Response<IEnumerable<CategoryDTO>>()
                {
                    Code = 400,
                    Message = ex.Message,
                    Status = false
                };
                return BadRequest(data);
            }
        }
        #endregion

        #region GetById
        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryAsync([FromRoute] int id)
        {
            try
            {
                var res = await services.category.Get(i => i.Id == id);
                if (res is null)
                    throw new Exception("Item Not Exist");

                var item = mapper.Map<CategoryDTO>(res);
                var data = new Response<CategoryDTO>()
                {
                    Code = 200,
                    Message = "OK",
                    Status = true,
                    Data = item
                };
                return Ok(data);
            }
            catch (Exception ex)
            {

                var data = new Response<CategoryDTO>()
                {
                    Code = 400,
                    Message = ex.Message,
                    Status = false
                };
                return BadRequest(data);
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{id:int}", Name = "DeleteCategoryById")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            try
            {
                var item = await services.category.Get(i => i.Id == id);
                if (item is null)
                    throw new Exception("Model Is Not Exist");

                services.category.Delete(item);
                await services.SaveChanges();
                var data = new Response<Category>()
                {
                    Code = 200,
                    Message = "Model Is Deleted",
                    Status = true,
                    Data = item
                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                var data = new Response<Category>()
                {
                    Code = 400,
                    Message = ex.Message,
                    Status = false
                };
                return BadRequest(data);
            }
        }
        #endregion

        #region Create
        [HttpPost]
        public async Task<IActionResult> Createcategory([FromForm] CategoryDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Model Is Not Valid");

                var category = mapper.Map<Category>(model);
                await services.category.Create(category);
                await services.SaveChanges();
                var data = new Response<CategoryDTO>()
                {
                    Code = 200,
                    Message = "Model Is Created",
                    Status = true,
                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                var data = new Response<CategoryDTO>()
                {
                    Code = 400,
                    Message = ex.Message,
                    Status = false
                };
                return BadRequest(data);
            }
        }
        #endregion

        #region Update
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Updatecategory([FromRoute] int id, [FromForm] CategoryDTO model)
        {
            try
            {
                var res = await services.category.Get(i => i.Id == id);
                if (res is null)
                    throw new Exception("Model Is Not Exist");

                var item = mapper.Map<Category>(model);
                item.Id = res.Id;
                 services.category.Update(item);
                await services.SaveChanges();
                var data = new Response<CategoryDTO>()
                {
                    Code = 200,
                    Message = "Model Is Updated",
                    Status = true,
                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                var data = new Response<CategoryDTO>()
                {
                    Code = 400,
                    Message = ex.Message,
                    Status = false
                };
                return BadRequest(data);
            }
        }
        #endregion

        #endregion
    }
}
