﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Model;
using Pokemon.Model.IRpo;
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

        #endregion

        #region Create

        #endregion

        #region Update

        #endregion

        #endregion
    }
}
