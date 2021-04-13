using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlueMile.Certification.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
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
        /// <param name="userDTO">
        ///     The <see cref="UserDTO"/> object with the user details to register.
        /// </param>
        /// <returns></returns>
        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            try
            {
                this.loggerService.LogInfo($"{this.GetControllerActionNames()}: Login Attempt from user {userDTO.EmailAddress}");

                string username = userDTO.EmailAddress;
                string password = userDTO.Password;
                var user = new IdentityUser
                {
                    Email = username,
                    UserName = username
                };

                var result = await this.userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    //this.loggerService.LogError($"{this.GetControllerActionNames()}: User Registration Failed for user: {userDTO.EmailAddress}");
                    foreach (var error in result.Errors)
                    {
                        this.loggerService.LogError($"{this.GetControllerActionNames()}: {error.Code} - {error.Description}");
                    }
                    return this.InternalError(new AuthenticationException($"User Registration Failed for user: {userDTO.EmailAddress}"));
                }
                else
                {
                    await this.userManager.AddToRoleAsync(user, "Customer");
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
        /// <param name="userDTO">
        /// </param>
        /// <returns></returns>
        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            try
            {
                this.loggerService.LogInfo($"{this.GetControllerActionNames()}: Login Attempt from user {userDTO.EmailAddress}");

                string username = userDTO.EmailAddress;
                string password = userDTO.Password;
                var result = await this.signInManager.PasswordSignInAsync(username, password, false, false);

                if (result.Succeeded)
                {
                    var user = await this.userManager.FindByNameAsync(userDTO.EmailAddress).ConfigureAwait(false);
                    var tokenString = await this.GenerateJSONWebToken(user);
                    this.loggerService.LogInfo($"{this.GetControllerActionNames()}: Successfully Authenticated {userDTO.EmailAddress}");

                    return Ok(new { token = tokenString });
                }
                else
                {
                    this.loggerService.LogWarning($"{this.GetControllerActionNames()}: Authentication Failed for user: {userDTO.EmailAddress}");
                    return Unauthorized(userDTO);
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
