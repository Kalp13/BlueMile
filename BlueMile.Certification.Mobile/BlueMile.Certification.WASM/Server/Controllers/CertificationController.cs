using BlueMile.Certification.WASM.Server.Data;
using BlueMile.Certification.WASM.Server.Services;
using BlueMile.Certification.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlueMile.Certification.WASM.Server.Controllers
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

            return Ok(owner);
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
        public async Task<IActionResult> UpdateOwner(Guid id, OwnerModel ownerEntity)
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
        public async Task<IActionResult> CreateOwner([FromBody] OwnerModel ownerEntity)
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

            return Ok(boats);
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

            var boats = await this.certificationRepository.FindBoatById(id);

            return Ok(boats);
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
        public async Task<IActionResult> UpdateBoat(Guid id, BoatModel boatEntity)
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
        public async Task<IActionResult> CreateBoat(BoatModel boatEntity)
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

        #region Instance Fields

        private readonly ICertificationRepository certificationRepository;

        #endregion
    }
}
