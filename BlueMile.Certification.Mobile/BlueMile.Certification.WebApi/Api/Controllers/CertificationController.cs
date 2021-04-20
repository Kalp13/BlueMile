using BlueMile.Certification.Web.ApiModels;
using BlueMile.Certification.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlueMile.Certification.WebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<IEnumerable<OwnerModel>>> GetOwners()
        {
            var owners = await this.certificationRepository.FindAllOwners();

            return Ok(owners);
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
        [HttpGet("owner/get/{id}")]
        public async Task<IActionResult> GetOwner(Guid? id)
        {
            if (!id.HasValue)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var owner = await this.certificationRepository.FindOwnerById(id.Value);

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
        [HttpPut("owner/update/{id}")]
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
        [HttpPost("owner/create")]
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
        [HttpDelete("owner/delete/{id}")]
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
        [HttpGet("boat/{ownerId}")]
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
        [HttpGet("boat/get/{id}")]
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
        [HttpPut("boat/update/{id}")]
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
        [HttpPost("boat/create")]
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
        [HttpDelete("boat/delete/{id}")]
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
        [HttpPut("boat/update/{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, UpdateItemModel itemEntity)
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
        [HttpPost("boat/create")]
        public async Task<IActionResult> CreateItem(CreateItemModel itemEntity)
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
        [HttpDelete("item/delete/{id}")]
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
