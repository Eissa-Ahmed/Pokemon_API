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
    public class OwnerController : ControllerBase
    {
        #region Ctor
        private readonly IUnitOfWork services;
        private readonly IMapper mapper;
        public OwnerController(IUnitOfWork services, IMapper mapper)
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
                var data = mapper.Map<IEnumerable<OwnerDTO>>(services.owner.GetAll());
                return Ok(new Response<IEnumerable<OwnerDTO>>() { 
                Code = 200,
                Status = true,
                Message = "Data Is Geted",
                Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<IEnumerable<OwnerDTO>>()
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }

        #endregion

        #region GetById
        [HttpGet("GetById/{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                if (!services.owner.OwnerExist(id))
                    return NotFound();

                var data = mapper.Map<OwnerDTO>(services.owner.Get(id));
                return Ok(new Response<OwnerDTO>()
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<OwnerDTO>()
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }

        #endregion

        #region GetOwnerByPokemon
        [HttpGet("GetOwnerByPokemon/{id:int}")]
        public IActionResult GetOwnerByPokemon(int id)
        {
            try
            {
                if (!services.pokemon.PokemonExists(id))
                    return NotFound();

                var data = mapper.Map<IEnumerable<OwnerDTO>>(services.owner.GetOwnerByPokemon(id));
                return Ok(new Response<IEnumerable<OwnerDTO>>()
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<IEnumerable<OwnerDTO>>()
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }

        #endregion

        #region GetOwnerByPokemon
        [HttpGet("GetPokemonsByOwner/{id:int}")]
        public IActionResult GetPokemonsByOwner(int id)
        {
            try
            {
                if (!services.owner.OwnerExist(id))
                    return NotFound();

                var data = mapper.Map<IEnumerable<PokemonDTO>>(services.owner.GetPokemonsByOwner(id));
                return Ok(new Response<IEnumerable<PokemonDTO>>()
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<IEnumerable<PokemonDTO>>()
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
        public async Task<IActionResult> DeleteOwner(int id)
        {
            try
            {
                if (!services.owner.OwnerExist(id))
                    return NotFound();

                services.owner.DeleteOwner(id);
                await services.SaveChanges();

                return Ok(new Response<IEnumerable<OwnerDTO>>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Deleted",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<IEnumerable<OwnerDTO>>
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
        public async Task<IActionResult> CreateOwner([FromForm] OwnerDTO model)
        {
            try
            {
                var owner = services.owner.GetAll().Where(i => i.Name.Trim().ToLower() == model.Name.ToLower()).FirstOrDefault();
                if (owner != null)
                    throw new Exception("owner Is Exist");

                services.owner.CreateOwner(mapper.Map<Owner>(model));
                await services.SaveChanges();

                return Ok(new Response<IEnumerable<OwnerDTO>>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Creatred",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<IEnumerable<OwnerDTO>>
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
        public async Task<IActionResult> UpdateOwner(int id, [FromForm] OwnerDTO model)
        {
            try
            {
                if (!services.owner.OwnerExist(id))
                    throw new Exception("Country Is Not Exist");

                services.owner.UpdateOwner(id, mapper.Map<Owner>(model));
                await services.SaveChanges();

                return Ok(new Response<IEnumerable<OwnerDTO>>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Updated",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<IEnumerable<OwnerDTO>>
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
