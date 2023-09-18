using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokemon.DAL.Helper;
using Pokemon.Model;
using Pokemon.Model.IRpo;
using Pokemon.Model.Models;
using Pokemon.Model.ModelsDTO;

namespace PokemonApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : ControllerBase
    {
        #region Ctor
        public readonly IUnitOfWork services;
        private readonly IMapper mapper;

        public ReviewerController(IUnitOfWork unitOfWork, IMapper mapper)
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
                var items = await services.reviewer.GetAllAsync("Reviews");
                var data = new Response<IEnumerable<Reviewer>>()
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

                var data = new Response<IEnumerable<Reviewer>>()
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
        [HttpGet("{id:int}", Name = "GetReviewerById")]
        public async Task<IActionResult> GetReviewerAsync([FromRoute] int id)
        {
            try
            {
                var item = await services.reviewer.Get(i => i.Id == id , "Reviews");
                if (item is null)
                    throw new Exception("Item Not Exist");

                var data = new Response<Reviewer>()
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

                var data = new Response<Reviewer>()
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
        [HttpDelete("{id:int}", Name = "DeleteReviewerById")]
        public async Task<IActionResult> DeleteReviewer([FromRoute] int id)
        {
            try
            {
                var item = await services.reviewer.Get(i => i.Id == id);
                if (item is null)
                    throw new Exception("Model Is Not Exist");

                services.reviewer.Delete(item);
                await services.SaveChanges();
                var data = new Response<Reviewer>()
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
                var data = new Response<Pokemons>()
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
        public async Task<IActionResult> CreateReviewer([FromForm] ReviewerDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Model Is Not Valid");

                var reviewer = mapper.Map<Reviewer>(model);
                await services.reviewer.Create(reviewer);
                await services.SaveChanges();
                var data = new Response<Reviewer>()
                {
                    Code = 200,
                    Message = "Model Is Created",
                    Status = true,
                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                var data = new Response<Reviewer>()
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
        public async Task<IActionResult> UpdateReviewer([FromRoute] int id, [FromForm] ReviewerDTO model)
        {
            try
            {
                var reviewer = await services.reviewer.Get(i => i.Id == id);
                if (reviewer is null)
                    throw new Exception("Model Is Not Exist");

                var item = mapper.Map<Reviewer>(model);
                item.Id = id;
                await services.reviewer.Update(item);
                await services.SaveChanges();
                var data = new Response<Reviewer>()
                {
                    Code = 200,
                    Message = "Model Is Updated",
                    Status = true,
                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                var data = new Response<Reviewer>()
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
