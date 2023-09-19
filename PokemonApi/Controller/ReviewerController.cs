using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Model;
using Pokemon.Model.IRpo;
using Pokemon.Model.ModelsDTO;

namespace PokemonApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : ControllerBase
    {
        #region Ctor
        private readonly IUnitOfWork services;
        private readonly IMapper mapper;
        public ReviewerController(IUnitOfWork services, IMapper mapper)
        {
            this.services = services;
            this.mapper = mapper;
        }
        #endregion

        #region Action

        #region GetAll

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                var data = mapper.Map<IEnumerable<ReviewerDTO>>(services.reviewer.GetAll());
                return Ok(new Response<IEnumerable<ReviewerDTO>>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<IEnumerable<ReviewerDTO>>
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }

        #endregion

        #region GetById

        [HttpGet("getById/{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                if (!services.reviewer.ReviewerExist(id))
                    return NotFound();

                var data = mapper.Map<ReviewerDTO>(services.reviewer.Get(id));
                return Ok(new Response<ReviewerDTO>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<ReviewerDTO>
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }

        #endregion


        #region Get Reviews By Reviewer

        [HttpGet("GetReviewsByReviewer/{id:int}")]
        public IActionResult GetReviewsByReviewer(int id)
        {
            try
            {
                if (!services.reviewer.ReviewerExist(id))
                    return NotFound();

                var data = mapper.Map<IEnumerable<ReviewDTO>>(services.reviewer.GetReviewsByReviewer(id));
                return Ok(new Response<IEnumerable<ReviewDTO>>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<IEnumerable<ReviewDTO>>
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
