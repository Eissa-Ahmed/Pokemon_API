﻿using AutoMapper;
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
        private readonly IUnitOfWork services;
        private readonly IMapper mapper;
        public ReviewController(IUnitOfWork services, IMapper mapper)
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
                var data = mapper.Map<IEnumerable<ReviewDTO>>(services.review.GetAll());
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

        #region GetById

        [HttpGet("getById/{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = mapper.Map<ReviewDTO>(services.review.GetById(id));
                return Ok(new Response<ReviewDTO>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<ReviewDTO>
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }

        #endregion


        #region Get Review By Pokemon

        [HttpGet("GetReviewByPokemon/{id:int}")]
        public IActionResult GetReviewByPokemon(int id)
        {
            try
            {
                var data = mapper.Map<IEnumerable<ReviewDTO>>(services.review.GetReviewByPokemon(id));
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


        #region Delete
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                if (!services.review.ReviewExist(id))
                    return NotFound();

                services.review.DeleteReview(id);
                await services.SaveChanges();

                return Ok(new Response<IEnumerable<ReviewDTO>>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Deleted",
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

        #region Create
        [HttpPost()]
        public async Task<IActionResult> CreateReview([FromForm] ReviewDTO model)
        {
            try
            {

                services.review.CreateReview(mapper.Map<Review>(model));
                await services.SaveChanges();

                return Ok(new Response<IEnumerable<ReviewDTO>>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Creatred",
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

        #region Update
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateReview(int id, [FromForm] ReviewDTO model)
        {
            try
            {
                if (!services.review.ReviewExist(id))
                    throw new Exception("Review Is Not Exist");

                services.review.UpdateReview(id, mapper.Map<Review>(model));
                await services.SaveChanges();

                return Ok(new Response<IEnumerable<ReviewDTO>>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Updated",
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
