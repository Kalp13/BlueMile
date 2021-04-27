using BlueMile.Certification.Data.Static;
using BlueMile.Certification.Web.ApiModels;
using BlueMile.Certification.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlueMile.Certification.WebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Constructor

        /// <summary>
        /// Instantiates a new default instance of the <see cref="UsersController"/>
        /// </summary>
        /// <param name="signIn"></param>
        /// <param name="manager"></param>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        public UsersController(SignInManager<IdentityUser> signIn,
                               UserManager<IdentityUser> manager,
                               ILoggerService logger,
                               IConfiguration config)
        {
            this.loggerService = logger;
            this.signInManager = signIn;
            this.userManager = manager;
            this.configuration = config;
        }

        #endregion

        #region Service Calls

        /// <summary>
        /// Registers a new user with the given properties.
        /// </summary>
        /// <param name="createUser">
        ///     The <see cref="UserDTO"/> object with the user details to register.
        /// </param>
        /// <returns></returns>
        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegistrationModel createUser)
        {
            try
            {
                this.loggerService.LogInfo($"{this.GetControllerActionNames()}: Login Attempt from user {createUser.EmailAddress}");

                string username = createUser.EmailAddress;
                string password = createUser.Password;
                var user = new IdentityUser
                {
                    Email = username,
                    UserName = username,
                    PhoneNumber = createUser.ContactNumber,
                };

                var result = await this.userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        this.loggerService.LogError($"{this.GetControllerActionNames()}: {error.Code} - {error.Description}");
                    }
                    return this.InternalError(new AuthenticationException($"User Registration Failed for user: {createUser.EmailAddress}"));
                }
                else
                {
                    await this.userManager.AddToRoleAsync(user, Enum.GetName(typeof(UserRoles), UserRoles.Owner));
                    return Ok(new { result.Succeeded });
                }
            }
            catch (Exception exc)
            {
                return this.InternalError(exc);
            }
        }

        /// <summary>
        /// Logs the user is with the given details.
        /// </summary>
        /// <param name="userModel">
        ///     The user details to log in with.
        /// </param>
        /// <returns></returns>
        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userModel)
        {
            try
            {
                this.loggerService.LogInfo($"{this.GetControllerActionNames()}: Login Attempt from user {userModel.EmailAddress}");

                string username = userModel.EmailAddress;
                string password = userModel.Password;
                var result = await this.signInManager.PasswordSignInAsync(username, password, false, false);

                if (result.Succeeded)
                {
                    var user = await this.userManager.FindByNameAsync(username).ConfigureAwait(false);
                    var tokenString = await this.GenerateJSONWebToken(user);
                    this.loggerService.LogInfo($"{this.GetControllerActionNames()}: Successfully Authenticated {user.Email}");

                    var roles = await this.userManager.GetRolesAsync(user);

                    return Ok(new UserToken()
                    { 
                        Token = tokenString,
                        Roles = roles.ToArray(),
                        Username = user.UserName
                    });
                }
                else
                {
                    this.loggerService.LogWarning($"{this.GetControllerActionNames()}: Authentication Failed for user: {userModel.EmailAddress}");
                    return Unauthorized(userModel);
                }
            }
            catch (Exception exc)
            {
                return this.InternalError(exc);
            }
        }

        #endregion

        #region Class Methods

        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;

            return $"{controller} - {action}";
        }

        private ObjectResult InternalError(Exception exc)
        {
            this.loggerService.LogError($"{this.GetControllerActionNames()} - {exc.Message} - {exc.InnerException}: {exc.InnerException?.Message}");

            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong. Please contact the Administrator.");
        }

        private async Task<string> GenerateJSONWebToken(IdentityUser user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
                var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                };

                var roles = await this.userManager.GetRolesAsync(user);
                claims.AddRange(roles.Select(x => new Claim(ClaimsIdentity.DefaultRoleClaimType, x)));

                var token = new JwtSecurityToken(this.configuration["JWT:Issuer"],
                                                this.configuration["JWT:Issuer"],
                                                claims,
                                                null,
                                                expires: DateTime.Now.AddMinutes(5),
                                                signingCredentials: credential);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #endregion

        #region Instance Fields

        private readonly SignInManager<IdentityUser> signInManager;

        private readonly UserManager<IdentityUser> userManager;

        private ILoggerService loggerService;

        private IConfiguration configuration;

        #endregion
    }
}
