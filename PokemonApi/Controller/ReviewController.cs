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
    public class ReviewController : ControllerBase
    {
        #region Ctor
        public readonly IUnitOfWork services;
        private readonly IMapper mapper;

        public ReviewController(IUnitOfWork unitOfWork, IMapper mapper)
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
                var items = await services.review.GetAllAsync("Reviewer");
                var data = new Response<IEnumerable<Review>>()
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

                var data = new Response<IEnumerable<Review>>()
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
        [HttpGet("{id:int}", Name = "GetReviewById")]
        public async Task<IActionResult> GetReviewAsync([FromRoute] int id)
        {
            try
            {
                var item = await services.review.Get(i => i.Id == id, "Reviewer");
                if (item is null)
                    throw new Exception("Item Not Exist");

                var data = new Response<Review>()
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

                var data = new Response<Review>()
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
        [HttpDelete("{id:int}", Name = "DeleteReviewById")]
        public async Task<IActionResult> DeleteReview([FromRoute] int id)
        {
            try
            {
                var res = await services.review.Get(i => i.Id == id);
                if (res is null)
                    throw new Exception("Model Is Not Exist");

                var item = mapper.Map<ReviewDTO>(res);
                services.review.Delete(res);
                await services.SaveChanges();
                var data = new Response<ReviewDTO>()
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
                var data = new Response<ReviewDTO>()
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
        public async Task<IActionResult> CreateReview([FromForm] ReviewDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Model Is Not Valid");

                var Review = mapper.Map<Review>(model);
                await services.review.Create(Review);
                await services.SaveChanges();
                var data = new Response<Review>()
                {
                    Code = 200,
                    Message = "Model Is Created",
                    Status = true,
                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                var data = new Response<Review>()
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
        public async Task<IActionResult> UpdateReview([FromRoute] int id, [FromForm] ReviewDTO model)
        {
            try
            {
                var Review = await services.review.Get(i => i.Id == id);
                if (Review is null)
                    throw new Exception("Model Is Not Exist");

                var item = mapper.Map<Review>(model);
                item.Id = id;
                await services.review.Update(item);
                await services.SaveChanges();
                var data = new Response<Review>()
                {
                    Code = 200,
                    Message = "Model Is Updated",
                    Status = true,
                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                var data = new Response<Review>()
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
