using BlueMile.Certification.Data;
using BlueMile.Certification.Data.Models;
using BlueMile.Certification.Data.Static;
using BlueMile.Certification.Web.ApiModels;
using BlueMile.Certification.WebApi.Infrastructure.Extensions;
using BlueMile.Certification.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
        public UsersController(SignInManager<ApplicationUser> signIn,
                               UserManager<ApplicationUser> manager,
                               IDbContextFactory<ApplicationDbContext> dbFactory,
                               ILogger<UsersController> logger,
                               IConfiguration config)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.signInManager = signIn ?? throw new ArgumentNullException(nameof(signIn));
            this.userManager = manager ?? throw new ArgumentNullException(nameof(manager));
            this.configuration = config ?? throw new ArgumentNullException(nameof(config));
            this.dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
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
                this.logger.TraceRequest(createUser);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                using ApplicationDbContext dbContext = this.dbFactory.CreateDbContext();

                string username = createUser.EmailAddress;
                string password = createUser.Password;
                var user = new ApplicationUser
                {
                    OwnerId = createUser.OwnerId,
                    Email = username,
                    UserName = username,
                    PhoneNumber = createUser.ContactNumber,
                };

                var result = await this.userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        this.logger.LogError($"{this.GetControllerActionNames()}: {error.Code} - {error.Description}");
                    }
                    return this.BadRequest(result);
                }
                else
                {
                    user = await this.userManager.FindByNameAsync(user.UserName);
                    dbContext.UserRoles.Add(new IdentityUserRole<Guid>() { RoleId = ApplicationRoleIdentifiers.OwnerUser, UserId = user.Id });

                    await dbContext.SaveChangesAsync();
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
                this.logger.TraceRequest(userModel);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                string username = userModel.EmailAddress;
                string password = userModel.Password;
                var result = await this.signInManager.PasswordSignInAsync(username, password, false, false);

                if (result.Succeeded)
                {
                    var user = await this.userManager.FindByNameAsync(username).ConfigureAwait(false);
                    var tokenString = await this.GenerateJSONWebToken(user);
                    this.logger.LogInformation($"{this.GetControllerActionNames()}: Successfully Authenticated {user.Email}");

                    var roles = await this.userManager.GetRolesAsync(user);

                    return Ok(new UserToken()
                    { 
                        Token = tokenString,
                        Roles = roles.ToArray(),
                        OwnerId = user.OwnerId.HasValue ? user.OwnerId.Value : Guid.Empty,
                        Username = user.UserName
                    });
                }
                else
                {
                    this.logger.LogWarning($"{this.GetControllerActionNames()}: Authentication Failed for user: {userModel.EmailAddress}");
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
            this.logger.LogError($"{this.GetControllerActionNames()} - {exc.Message} - {exc.InnerException}: {exc.InnerException?.Message}");

            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong. Please contact the Administrator.");
        }

        private async Task<string> GenerateJSONWebToken(ApplicationUser user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
                var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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

        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Used for logging events
        /// </summary>
        private readonly ILogger<UsersController> logger;

        private IConfiguration configuration;

        /// <summary>
        /// Provides access to the underlying data store.
        /// </summary>
        private IDbContextFactory<ApplicationDbContext> dbFactory;

        #endregion
    }
}
