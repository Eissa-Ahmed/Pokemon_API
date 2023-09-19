using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Model;
using Pokemon.Model.IRpo;
using Pokemon.Model.ModelsDTO;

namespace PokemonApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        #region Ctor
        public readonly IUnitOfWork services;
        private readonly IMapper mapper;

        public CountryController(IUnitOfWork services, IMapper mapper)
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
                var data = mapper.Map<IEnumerable<CountryDTO>>(services.country.GetAll());
                return Ok(new Response<IEnumerable<CountryDTO>>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<IEnumerable<CountryDTO>>
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
                var data = mapper.Map<CountryDTO>(services.country.Get(id));
                if (data is null)
                    return NotFound();

                return Ok(new Response<CountryDTO>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<CountryDTO>
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }
        #endregion

        #region Get Country By Owner
        [HttpGet("CountryByOwner/{id:int}")]
        public IActionResult GetCountryByOwner(int id)
        {
            try
            {
                
                var data = mapper.Map<CountryDTO>(services.country.GetCountryByOwner(id));
                if (data is null)
                    return NotFound();

                return Ok(new Response<CountryDTO>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<CountryDTO>
                {
                    Code = 400,
                    Status = false,
                    Message = ex.Message,
                });
            }
        }
        #endregion

        #region Get Owner By Country
        [HttpGet("GetOwnerFromCountry/{id:int}")]
        public IActionResult GetOwnerByCountry(int id)
        {
            try
            {
                if (!services.country.CountryExist(id))
                    return NotFound();

                var data = mapper.Map<IEnumerable<OwnerDTO>>(services.country.GetOwnersFromCountery(id));
                if (data is null)
                    return NotFound();

                return Ok(new Response<IEnumerable<OwnerDTO>>
                {
                    Code = 200,
                    Status = true,
                    Message = "Data Is Geted",
                    Data = data
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

        #region Delete

        #endregion

        #region Create

        #endregion

        #region Update

        #endregion

        #endregion
    }
}
