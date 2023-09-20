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
    public class PokemonController : ControllerBase
    {
        #region Ctor
        public readonly IUnitOfWork services;
        private readonly IMapper mapper;

        public PokemonController(IUnitOfWork unitOfWork, IMapper mapper)
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
                return Ok(new Response<IEnumerable<Pokemons>>
                {
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
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            try
            {

                if (!services.pokemon.PokemonExists(id))
                    throw new Exception("Pokemon Is Not Exist");

                FileManager.RemoveImage(services.pokemon.Get(id).ImageUrl);

                services.pokemon.DeletePokemon(id);
                await services.SaveChanges();

                return Ok(new Response<Pokemons>()
                {
                    Code = 200,
                    Status = true,
                    Message = "Pokemon Is Deleted",
                });
            }
            catch (Exception ex)
            {
                return Ok(new Response<Pokemons>()
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }
        #endregion

        #region Create
        [HttpPost]
        public async Task<IActionResult> CreatePokemon([FromForm] PokemonCreateDTO model)
        {
            try
            {
                if (services.pokemon.GetAll().Where(i => i.Name.Trim().ToLower() == model.Name.Trim().ToLower()).FirstOrDefault() != null)
                    throw new Exception("Pokemon Is Exist");

                if(!services.owner.OwnerExist(model.OwnerId))
                    throw new Exception("Owner Is Not Exist");

                if (!services.category.CategoryExists(model.CategoryId))
                    throw new Exception("Category Is Not Exist");

                string imageUrl = await FileManager.UploadImage(model.file);
                var item = mapper.Map<Pokemons>(model);
                item.ImageUrl = imageUrl;
                services.pokemon.CreatePokemon(model.OwnerId, model.CategoryId, item);
                await services.SaveChanges();

                return Ok(new Response<Pokemons>() {
                    Code = 200,
                    Status = true,
                    Message = "Pokemon Is Created",
                });
            }
            catch (Exception ex)
            {
                return Ok(new Response<Pokemons>()
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
        public async Task<IActionResult> UpdatePokemon(int id , [FromForm] PokemonUpdateDTO model)
        {
            try
            {

                if (!services.pokemon.PokemonExists(id))
                    throw new Exception("Pokemon Is Not Exist");

                var item = mapper.Map<Pokemons>(model);
                if (model.file != null)
                {
                    FileManager.RemoveImage(services.pokemon.Get(id).ImageUrl);
                    string imageUrl = await FileManager.UploadImage(model.file);
                    item.ImageUrl = imageUrl;
                }
                services.pokemon.UpdatePokemon(id, item);

                await services.SaveChanges();

                return Ok(new Response<Pokemons>()
                {
                    Code = 200,
                    Status = true,
                    Message = "Pokemon Is Updated",
                });
            }
            catch (Exception ex)
            {
                return Ok(new Response<Pokemons>()
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
