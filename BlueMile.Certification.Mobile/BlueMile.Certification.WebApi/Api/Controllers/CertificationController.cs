﻿using BlueMile.Certification.Data.Static;
using BlueMile.Certification.Web.ApiModels;
using BlueMile.Certification.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlueMile.Certification.WebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class CertificationController : ControllerBase
    {
        #region Constructor

        /// <summary>
        /// Creates a new default instance of <see cref="CertificationController"/>.
        /// </summary>
        public CertificationController(ICertificationRepository certificationRepository)
        {
            this.certificationRepository = certificationRepository ?? throw new ArgumentNullException(nameof(certificationRepository));
        }

        #endregion

        #region Owner Methods

        /// <summary>
        /// Gets all the active owners in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("owner")]
        [Authorize(Roles = nameof(UserRoles.Owner))]
        public async Task<ActionResult<IEnumerable<OwnerModel>>> GetOwners()
        {
            var owners = await this.certificationRepository.FindAllOwners();

            return Ok(owners);
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
        [Authorize(Roles = nameof(UserRoles.Owner) + ", " + nameof(UserRoles.Administrator))]
        public async Task<ActionResult<OwnerModel>> GetOwnerByUsername(string username)
        {
            if (String.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            var owner = await this.certificationRepository.FindOwnerByUsername(username);

            return Ok(owner);
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
        [Authorize(Roles = nameof(UserRoles.Owner) + ", " + nameof(UserRoles.Administrator))]
        public async Task<IActionResult> GetOwner(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var owner = await this.certificationRepository.FindOwnerById(Guid.Parse(id));

            if (owner != null)
            {
                return Ok(owner);
            }
            else
            {
                return NotFound();
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
        [Authorize]
        public async Task<IActionResult> UpdateOwner(Guid id, [FromBody] UpdateOwnerModel ownerEntity)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (ownerEntity == null)
            {
                throw new ArgumentNullException(nameof(ownerEntity));
            }

            var owner = await this.certificationRepository.UpdateOwner(ownerEntity);

            return Ok(owner);
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
            if (ownerEntity == null)
            {
                throw new ArgumentNullException(nameof(ownerEntity));
            }

            var result = await this.certificationRepository.CreateOwner(ownerEntity);

            return Ok(result);
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
        [Authorize]
        public async Task<IActionResult> DeleteOwner(Guid? id)
        {
            if (!id.HasValue || id.Value == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await this.certificationRepository.DeleteOwner(id.Value);
            return Ok(result);
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
        [Authorize/*(Roles = nameof(UserRoles.Owner) + ", " + nameof(UserRoles.Administrator))*/]
        public async Task<IActionResult> GetBoatsByOwnerId(Guid ownerId)
        {
            if (ownerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(ownerId));
            }

            var boats = await this.certificationRepository.FindAllBoatsByOwnerId(ownerId);

            if (boats != null && boats.Count > 0)
            {
                return Ok(boats);
            }
            else
            {
                return NotFound();
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
        [Authorize(Roles = nameof(UserRoles.Owner) + ", " + nameof(UserRoles.Administrator))]
        public async Task<IActionResult> GetBoat(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var boat = await this.certificationRepository.FindBoatById(id);

            if (boat != null)
            {
                return Ok(boat);
            }
            else
            {
                return NotFound();
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
        [Authorize(Roles = nameof(UserRoles.Owner) + ", " + nameof(UserRoles.Administrator))]
        public async Task<IActionResult> UpdateBoat(Guid id, [FromBody] UpdateBoatModel boatEntity)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (boatEntity == null)
            {
                throw new ArgumentNullException(nameof(boatEntity));
            }

            var boat = await this.certificationRepository.UpdateBoat(boatEntity);

            return Ok(boat);
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
        [Authorize(Roles = nameof(UserRoles.Owner) + ", " + nameof(UserRoles.Administrator))]
        public async Task<IActionResult> CreateBoat([FromBody] CreateBoatModel boatEntity)
        {
            if (boatEntity == null)
            {
                throw new ArgumentNullException(nameof(boatEntity));
            }

            var result = await this.certificationRepository.CreateBoat(boatEntity);

            return Ok(result);
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
        [Authorize(Roles = nameof(UserRoles.Owner) + ", " + nameof(UserRoles.Administrator))]
        public async Task<IActionResult> DeleteBoat(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await this.certificationRepository.DeleteBoat(id);
            return Ok(result);
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
        [Authorize/*(Roles = nameof(UserRoles.Owner) + ", " + nameof(UserRoles.Administrator))*/]
        public async Task<IActionResult> GetItemsByBoatId(Guid boatId)
        {
            if (boatId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(boatId));
            }

            var items = await this.certificationRepository.FindItemsByBoatId(boatId);

            if (items != null && items.Count > 0)
            {
                return Ok(items);
            }
            else
            {
                return NotFound();
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
        [Authorize(Roles = nameof(UserRoles.Owner) + ", " + nameof(UserRoles.Administrator))]
        public async Task<IActionResult> GetItem(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var boat = await this.certificationRepository.FindBoatById(id);

            if (boat != null)
            {
                return Ok(boat);
            }
            else
            {
                return NotFound();
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
        [Authorize/*(Roles = nameof(UserRoles.Owner) + ", " + nameof(UserRoles.Administrator))*/]
        public async Task<IActionResult> UpdateItem(Guid id, [FromBody] UpdateItemModel itemEntity)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (itemEntity == null)
            {
                throw new ArgumentNullException(nameof(itemEntity));
            }

            var result = await this.certificationRepository.UpdateItem(itemEntity);

            return Ok(result);
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
        [Authorize(Roles = nameof(UserRoles.Owner) + ", " + nameof(UserRoles.Administrator))]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemModel itemEntity)
        {
            if (itemEntity == null)
            {
                throw new ArgumentNullException(nameof(itemEntity));
            }

            var result = await this.certificationRepository.CreateItem(itemEntity);

            return Ok(result);
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
        [Authorize(Roles = nameof(UserRoles.Owner) + ", " + nameof(UserRoles.Administrator))]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await this.certificationRepository.DeleteItem(id);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        #endregion

        #region Instance Fields

        private readonly ICertificationRepository certificationRepository;

        #endregion
    }
}
