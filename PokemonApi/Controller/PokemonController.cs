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

        public PokemonController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.services = unitOfWork;
            this.mapper = mapper;
        }
        #endregion

        #region Action

        #region Rates
        [HttpGet("rate/{id:int}")]
        public async Task<IActionResult> Rates(int id)
        {
            var rates = await services.pokemon.GetRate(id);
            return Ok(new Response<double>
            {
                Code = 200,
                Message = "Ok",
                Status = true,
                Data = rates
            });
        }
        #endregion

        #region GetAllAsync
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var res = await services.pokemon.GetAllAsync("Review");
                var items = mapper.Map<IEnumerable<PokemonGetDTO>>(res);
                var data = new Response<IEnumerable<PokemonGetDTO>>()
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

                var data = new Response<IEnumerable<Pokemons>>()
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
        [HttpGet("{id:int}" , Name = "GetById")]
        public async Task<IActionResult> GetPokemonAsync([FromRoute]int id)
        {
            try
            {
                var res = await services.pokemon.Get(i => i.Id == id);
                if (res is null)
                    throw new Exception("Item Not Exist");

                var item = mapper.Map<PokemonGetDTO>(res);
                var data = new Response<PokemonGetDTO>()
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

        #region Delete
        [HttpDelete("{id:int}", Name = "DeleteById")]
        public async Task<IActionResult> DeletePokemon([FromRoute] int id)
        {
            try
            {
                var item = await services.pokemon.Get(i => i.Id ==id);
                if (item is null)
                    throw new Exception("Model Is Not Exist");

                FileManager.RemoveImage(item.ImageUrl);
                services.pokemon.Delete(item);
                await services.SaveChanges();
                var data = new Response<Pokemons>()
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
        public async Task<IActionResult> CreatePokemon([FromForm] PokemonDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Model Is Not Valid");

                string imageUrl = await FileManager.UploadImage(model.file);
                var pokemon = mapper.Map<Pokemons>(model);
                pokemon.ImageUrl = imageUrl;
                await services.pokemon.Create(pokemon);
                await services.SaveChanges();
                var data = new Response<Pokemons>()
                {
                    Code = 200,
                    Message = "Model Is Created",
                    Status = true,
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

        #region Update
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePokemon([FromRoute] int id , [FromForm]PokemonDTO model)
        {
            try
            {
                var item = await services.pokemon.Get(i => i.Id == id);
                if (item is null)
                    throw new Exception("Model Is Not Exist");
                
                if (model.file is null)
                {
                    item.Name = model.Name;
                    item.BirthDate = model.BirthDate;
                    await services.pokemon.Update(item);
                }
                else
                {
                    FileManager.RemoveImage(item.ImageUrl);
                    item.Name = model.Name;
                    item.BirthDate = model.BirthDate;
                    item.ImageUrl = await FileManager.UploadImage(model.file);
                    await services.pokemon.Update(item);
                }
                await services.SaveChanges();
                var data = new Response<Pokemons>()
                {
                    Code = 200,
                    Message = "Model Is Updated",
                    Status = true,
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

        #endregion

    }
}
