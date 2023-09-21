using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pokemon.DAL.Database;
using Pokemon.Model;
using Pokemon.Model.ModelsDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PokemonApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Ctor
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private IConfiguration config;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signManager, RoleManager<IdentityRole> roleManager, IMapper mapper, ApplicationDbContext dbContext, IConfiguration config)
        {
            this.userManager = userManager;
            this.signManager = signManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.config = config;
        }

        #endregion

        #region Action


        #region Register
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = mapper.Map<IdentityUser>(model);

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                ModelState.AddModelError("", result.Errors.First().Description);

            return Ok(new Response<RegisterDTO>
            {
                Code = 200,
                Status = true,
                Message = "Account Is Created",
                Data = model
            });
        }
        #endregion

        #region Sign In
        [HttpPost("Signin")]
        public async Task<IActionResult> Login( LoginDTO model)
        {
            if (!ModelState.IsValid)
                return Unauthorized();

            var user = await userManager.Users.FirstOrDefaultAsync(i => i.UserName == model.UserName);
            if (user is null)
                return Unauthorized();

            bool check = await userManager.CheckPasswordAsync(user, model.password);
            if (!check)
            {
                ModelState.AddModelError("", "User Or Password are Not Valid");
                return BadRequest(ModelState);
            }

            #region Token

            #region Claims
            var Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            #endregion

            #region Roles
            var roles = await userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, item));
            }
            #endregion

            #region signingCredentials
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecretKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            #endregion

            #region JwT
            JwtSecurityToken token = new JwtSecurityToken
                (
                issuer: config["JWT:issuer"], // web Api
                audience: config["JWT:audience"], // Consumer
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials
                );
            #endregion

            #endregion

            var result = await signManager.PasswordSignInAsync(user, model.password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "User Or Password Are Not Valid");
                return Unauthorized();
            }

            return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            /*return Ok(new Response<IdentityUser>
            {
                Code = 200,
                Status = true, 
                Message = "Account Is Login",
                Data = user
            });*/
        }
        #endregion


        #region Logout

        #endregion


        #region Delete User
        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string UserName)
        {
            var user = await dbContext.Users.Where(i => i.UserName == UserName).FirstOrDefaultAsync();
            if (user is null)
                return NotFound();

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
                ModelState.AddModelError("", result.Errors.First().Description);

            return Ok(new Response<string>
            {
                Code = 200,
                Status = true,
                Message = "Account Is Deleted",
                Data = user.Email
            });
        }
        #endregion


        #region Update User

        #endregion

        #region Add Role

        #endregion


        #endregion
    }
}
