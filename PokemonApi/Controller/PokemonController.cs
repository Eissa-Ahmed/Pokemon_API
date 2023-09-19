﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Model;
using Pokemon.Model.IRpo;
using Pokemon.Model.Models;
using Pokemon.Model.ModelsDTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PokemonApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        #region Ctor
        public readonly IUnitOfWork services;
        private readonly IMapper mapper;

        public PokemonController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.services = unitOfWork;
            this.mapper = mapper;
        }
        #endregion

        #region Action

        #region Rates
        [HttpGet("GetRate/{id:int}")]
        public IActionResult GetRates(int id)
        {
            try
            {
                var pokemon = services.pokemon.PokemonExists(id);
                if (!pokemon)
                    return NotFound();

                var data = services.pokemon.GetRate(id);
                return Ok(new Response<double>
                {
                    Code = 200,
                    Status = true,
                    Message = "Rates Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<double>
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
                var data = services.pokemon.GetAll();
                return Ok(new Response<IEnumerable<Pokemons>> { 
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<IEnumerable<Pokemons>>
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
                var data = mapper.Map<PokemonDTO>(services.pokemon.Get(id));
                if(data is null)
                    return NotFound();

                return Ok(new Response<PokemonDTO>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<PokemonDTO>
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }
        #endregion

        #region GetByName
        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var data = mapper.Map<PokemonDTO>(services.pokemon.Get(name));
                if (data is null)
                    return NotFound();

                return Ok(new Response<PokemonDTO>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<PokemonDTO>
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
