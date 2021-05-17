using BlueMile.Certification.Data.Static;
using BlueMile.Certification.Web.ApiModels;
using BlueMile.Certification.WebApi.Infrastructure.Extensions;
using BlueMile.Certification.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlueMile.Certification.WebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class CertificationController : ControllerBase
    {
        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="CertificationController"/>.
        /// </summary>
        public CertificationController(ICertificationRepository certificationRepository,
                                       ILogger<CertificationController> logger)
        {
            this.certificationRepository = certificationRepository ?? throw new ArgumentNullException(nameof(certificationRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Owner Methods

        /// <summary>
        /// Gets all the active owners in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("owner")]
        public async Task<ActionResult<IEnumerable<OwnerModel>>> GetOwners([FromQuery]FindOwnerModel findOwnerModel)
        {
            try
            {
                this.logger.TraceRequest(nameof(GetOwners));
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                var owners = await this.certificationRepository.FindAllOwners(findOwnerModel);

                this.logger.LogInformation($"Successfully retrieved {owners.Count} owners.");

                return this.Ok(owners);
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        /// <summary>
        /// Gets a certain owner with the given username.
        /// </summary>
        /// <param name="username">
        ///     The username of the owner to find.
        /// </param>
        /// <returns></returns>
        [HttpGet]
        [Route("owner/get/{username}")]
        public async Task<ActionResult<OwnerModel>> GetOwnerByUsername(string username)
        {
            try
            {
                this.logger.TraceRequest(username);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                if (String.IsNullOrWhiteSpace(username))
                {
                    throw new ArgumentNullException(nameof(username));
                }

                var owner = await this.certificationRepository.FindOwnerByUsername(username);

                if (owner != null)
                {
                    this.logger.LogInformation($"Successfully found owner with username {username} - {owner.Name} {owner.Surname}");

                    return this.Ok(owner);
                }
                else
                {
                    this.logger.LogInformation($"No owner found with username {username}");

                    return this.NotFound("No owner found with username: " + username);
                }
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        /// <summary>
        /// Gets the owner with the corresponding unique identifier.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the owner to find.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="OwnerModel"/> with the given owner identifier.
        /// </returns>
        [HttpGet]
        [Route("owner/{id}")]
        public async Task<IActionResult> GetOwner(string id)
        {
            try
            {
                this.logger.TraceRequest(id);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                if (String.IsNullOrWhiteSpace(id))
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var owner = await this.certificationRepository.FindOwnerById(Guid.Parse(id));

                if (owner != null)
                {
                    this.logger.LogInformation($"Successfully found owner with unique identifier {id} - {owner.Name} {owner.Surname}");

                    return this.Ok(owner);
                }
                else
                {
                    this.logger.LogInformation($"No owner found with unique identifier {id}");

                    return this.NotFound("No owner found with the given identifier: " + id);
                }
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        /// <summary>
        /// Updates an owner with the given unique identifier with the given properties.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the owner that needs to be updated.
        /// </param>
        /// <param name="ownerEntity">
        ///     The owner details to update to.
        /// </param>
        /// <returns></returns>
        [HttpPut]
        [Route("owner/update/{id}")]
        public async Task<IActionResult> UpdateOwner(Guid id, [FromBody] UpdateOwnerModel ownerEntity)
        {
            try
            {
                this.logger.TraceRequest(ownerEntity);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                if (ownerEntity == null)
                {
                    throw new ArgumentNullException(nameof(ownerEntity));
                }

                var ownerId = await this.certificationRepository.UpdateOwner(ownerEntity);

                if (ownerId != Guid.Empty)
                {
                    this.logger.LogInformation($"Successfully updates owner {ownerEntity.Name} {ownerEntity.Surname}");

                    return this.Ok(ownerId);
                }
                else
                {
                    this.logger.LogInformation($"Could not update owner {ownerEntity.Name} {ownerEntity.Surname}");

                    return this.NotFound("No owner found with the given details.");
                }
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        /// <summary>
        /// Creates a new owner with the given details.
        /// </summary>
        /// <param name="ownerEntity">
        ///     The <see cref="OwnerModel"/> to create.
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("owner/create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateOwner([FromBody] CreateOwnerModel ownerEntity)
        {
            try
            {
                this.logger.TraceRequest(ownerEntity);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                if (ownerEntity == null)
                {
                    throw new ArgumentNullException(nameof(ownerEntity));
                }

                var ownerId = await this.certificationRepository.CreateOwner(ownerEntity);

                if (ownerId != Guid.Empty)
                {
                    this.logger.LogInformation($"Successfully created new owner {ownerEntity.Name} {ownerEntity.Surname} with unique system identifier {ownerId}");
                    return this.Ok(ownerId);
                }
                else
                {
                    this.logger.LogInformation($"Could not create new owner {ownerEntity.Name} {ownerEntity.Surname}.");
                    return this.BadRequest($"Could not create new owner with given details.");
                }
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        /// <summary>
        /// Deletes an owner with the given unique identifier.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the owner.
        /// </param>
        /// <returns></returns>
        [HttpDelete]
        [Route("owner/delete/{id}")]
        public async Task<IActionResult> DeleteOwner(Guid? id)
        {
            try
            {
                this.logger.TraceRequest(id);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                if (!id.HasValue || id.Value == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var result = await this.certificationRepository.DeleteOwner(id.Value);

                if (result)
                {
                    this.logger.LogInformation($"Successfully deleted owner with unique system identifier {id}");
                    return this.Ok(result);
                }
                else
                {
                    this.logger.LogError($"Could not delete user with unique system identifier {id}");
                    return this.BadRequest("Could not delete owner.");
                }
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        #endregion

        #region Boat Methods

        /// <summary>
        /// Gets all the boats with the corresponding unique owner identifier.
        /// </summary>
        /// <param name="ownerId">
        ///     The unique identifier of the owner.
        /// </param>
        /// <returns></returns>
        [HttpGet]
        [Route("boat/{ownerId}")]
        public async Task<IActionResult> GetBoatsByOwnerId(Guid ownerId)
        {
            try
            {
                this.logger.TraceRequest(ownerId);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                if (ownerId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(ownerId));
                }

                var boats = await this.certificationRepository.FindAllBoatsByOwnerId(ownerId);

                if (boats != null)
                {
                    this.logger.LogInformation($"Successfully found {boats.Count} for owner with unique system identifier {ownerId}");
                    return this.Ok(boats);
                }
                else
                {
                    throw new ArgumentNullException(nameof(boats));
                }
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        /// <summary>
        /// Gets a <see cref="BoatModel"/> with the given unique identifier.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the boat to retrieve.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="BoatModel"/> with the given unique identifier.
        /// </returns>
        [HttpGet]
        [Route("boat/get/{id}")]
        public async Task<IActionResult> GetBoat(Guid id)
        {
            try
            {
                this.logger.TraceRequest(id);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var boat = await this.certificationRepository.FindBoatById(id);

                if (boat != null)
                {
                    this.logger.LogInformation($"Successfully found boat {boat.RegisteredNumber} with unique system identifier {id}");
                    return this.Ok(boat);
                }
                else
                {
                    this.logger.LogInformation($"No boat found with the given unique system identifier {id}");
                    return this.NotFound();
                }
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        /// <summary>
        /// Updates an existing boat with the corresponsing unique identifier with the given details.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the boat.
        /// </param>
        /// <param name="boatEntity">
        ///     The details to update the boat with.
        /// </param>
        /// <returns></returns>
        [HttpPut]
        [Route("boat/update/{id}")]
        public async Task<IActionResult> UpdateBoat(Guid id, [FromBody] UpdateBoatModel boatEntity)
        {
            try
            {
                this.logger.TraceRequest(boatEntity);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                if (boatEntity == null)
                {
                    throw new ArgumentNullException(nameof(boatEntity));
                }

                var boatId = await this.certificationRepository.UpdateBoat(boatEntity);

                if (boatId != Guid.Empty)
                {
                    this.logger.LogInformation($"Successfully updated boat {boatEntity.RegisteredNumber} with unique identifier {boatId}");
                    return this.Ok(boatId);
                }
                else
                {
                    this.logger.LogInformation($"Could not update boat {boatEntity.RegisteredNumber}");
                    return this.BadRequest("Could not update boat with given details.");
                }
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        /// <summary>
        /// Creates a new boat entity with the given properties.
        /// </summary>
        /// <param name="boatEntity">
        ///     The new boat properties to create with.
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("boat/create")]
        public async Task<IActionResult> CreateBoat([FromBody] CreateBoatModel boatEntity)
        {
            try
            {
                this.logger.TraceRequest(boatEntity);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                if (boatEntity == null)
                {
                    throw new ArgumentNullException(nameof(boatEntity));
                }

                var boatId = await this.certificationRepository.CreateBoat(boatEntity);

                if (boatId != Guid.Empty)
                {
                    this.logger.LogInformation($"Successfully created new boat {boatEntity.RegisteredNumber} with unique identifier {boatId}");
                    return this.Ok(boatId);
                }
                else
                {
                    this.logger.LogInformation($"Could not create new boat {boatEntity.RegisteredNumber}");
                    return BadRequest("Could not create new boat with the given details");
                }
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        /// <summary>
        /// Deletes a boat with the given unique identifier.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the boat.
        /// </param>
        /// <returns></returns>
        [HttpDelete]
        [Route("boat/delete/{id}")]
        public async Task<IActionResult> DeleteBoat(Guid id)
        {
            try
            {
                this.logger.TraceRequest(id);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var result = await this.certificationRepository.DeleteBoat(id);
                
                if (result)
                {
                    this.logger.LogInformation($"Successfully deleted boat with unique system identifier {id}");
                    return this.Ok(result);
                }
                else
                {
                    this.logger.LogError($"Could not delete boat with unique system identifier {id}");
                    return this.BadRequest("Could not delete boat.");
                }
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        #endregion

        #region Item Methods

        /// <summary>
        /// Gets list of items with the boat unique identifier given.
        /// </summary>
        /// <param name="boatId">
        ///     The unique identifier of the boat.
        /// </param>
        /// <returns></returns>
        [HttpGet]
        [Route("item/{boatId}")]
        public async Task<IActionResult> GetItemsByBoatId(Guid boatId)
        {
            try
            {
                this.logger.TraceRequest(boatId);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                if (boatId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(boatId));
                }

                var items = await this.certificationRepository.FindItemsByBoatId(boatId);

                if (items != null)
                {
                    this.logger.LogInformation($"Successfully retrieved {items.Count} items for boat with unique system identifier {boatId}");
                    return this.Ok(items);
                }
                else
                {
                    throw new ArgumentNullException(nameof(items));
                }
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        /// <summary>
        /// Gets a specific item with the given unique identifier specified.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the item.
        /// </param>
        /// <returns></returns>
        [HttpGet]
        [Route("item/{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            try
            {
                this.logger.TraceRequest(id);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var item = await this.certificationRepository.FindItemById(id);

                if (item != null)
                {
                    this.logger.LogInformation($"Successfully retrieved item {item} with unique system identifier {id}");
                    return this.Ok(item);
                }
                else
                {
                    this.logger.LogInformation($"No item found with unique system identifier {id}");
                    return this.NotFound();
                }
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        /// <summary>
        /// Updates an existing boat with the corresponsing unique identifier with the given details.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the boat.
        /// </param>
        /// <param name="itemEntity">
        ///     The details to update the boat with.
        /// </param>
        /// <returns></returns>
        [HttpPut]
        [Route("item/update/{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, [FromBody] UpdateItemModel itemEntity)
        {
            try
            {
                this.logger.TraceRequest(id);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                if (itemEntity == null)
                {
                    throw new ArgumentNullException(nameof(itemEntity));
                }

                var itemId = await this.certificationRepository.UpdateItem(itemEntity);

                if (itemId != Guid.Empty)
                {
                    this.logger.LogInformation($"Successfully updated {itemEntity.Description} with unique system identifier {itemId}");
                    return this.Ok(itemId);
                }
                else
                {
                    this.logger.LogError($"Could not update item {itemEntity.Description}");
                    return this.BadRequest("Could not update item.");
                }
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        /// <summary>
        /// Creates a new boat entity with the given properties.
        /// </summary>
        /// <param name="itemEntity">
        ///     The new boat properties to create with.
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("item/create")]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemModel itemEntity)
        {
            try
            {
                this.logger.TraceRequest(itemEntity);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                if (itemEntity == null)
                {
                    throw new ArgumentNullException(nameof(itemEntity));
                }

                var itemId = await this.certificationRepository.CreateItem(itemEntity);

                if (itemId != Guid.Empty)
                {
                    this.logger.LogInformation($"Successfully created new item {itemEntity.Description} with unique system identifier {itemId}");
                    return this.Ok(itemId);
                }
                else
                {
                    this.logger.LogError($"Could not create new item {itemEntity.Description}");
                    return this.BadRequest("Could not create new item.");
                }
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        /// <summary>
        /// Deletes an item with the given unique identifier.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of the item.
        /// </param>
        /// <returns></returns>
        [HttpDelete]
        [Route("item/delete/{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            try
            {
                this.logger.TraceRequest(id);
                this.logger.LogInformation($"{this.GetControllerActionNames()}: Attempting call.");

                if (id == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                var result = await this.certificationRepository.DeleteItem(id);

                if (result)
                {
                    this.logger.LogInformation($"Successfully deleted item with unique identifier {id}");
                    return this.Ok(result);
                }
                else
                {
                    this.logger.LogInformation($"Could not delete item with unique system idenfitier {id}");
                    return this.NotFound();
                }
            }
            catch (Exception exc)
            {
                this.logger.LogError(exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        #endregion

        #region Instance Methods
        

		/// <summary>
		/// Gets the name of the action where an error occurs.
		/// </summary>
		/// <returns>
		///		Returns the string name of the action where the error occurred.
		/// </returns>
		private string GetControllerActionNames()
		{
			var controller = this.ControllerContext.ActionDescriptor.ControllerName;
			var action = this.ControllerContext.ActionDescriptor.ActionName;

			return $"{controller} - {action}";
		}
        #endregion

        #region Instance Fields

        private readonly ICertificationRepository certificationRepository;

        /// <summary>
        /// Used for logging events
        /// </summary>
        private readonly ILogger<CertificationController> logger;

        #endregion
    }
}
