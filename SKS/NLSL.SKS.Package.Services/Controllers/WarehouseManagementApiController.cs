/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.20.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

#nullable enable

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using NLSL.SKS.Package.BusinessLogic.Entities;
using NLSL.SKS.Package.BusinessLogic.Interfaces;
using NLSL.SKS.Package.Services.Attributes;

using Swashbuckle.AspNetCore.Annotations;

using Error = NLSL.SKS.Package.Services.DTOs.Error;
using Warehouse = NLSL.SKS.Package.Services.DTOs.Warehouse;

namespace NLSL.SKS.Package.Services.Controllers
{
    /// <summary>
    /// </summary>
    [ApiController]
    public class WarehouseManagementApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWarehouseLogic _warehouseLogic;

        public WarehouseManagementApiController(IWarehouseLogic warehouseLogic, IMapper mapper)
        {
            _warehouseLogic = warehouseLogic;
            _mapper = mapper;
        }
        /// <summary>
        /// Exports the hierarchy of Warehouse and Truck objects.
        /// </summary>
        /// <response code="200">Successful response</response>
        /// <response code="400">An error occurred loading.</response>
        /// <response code="404">No hierarchy loaded yet.</response>
        [HttpGet]
        [Route("/warehouse")]
        [ValidateModelState]
        [SwaggerOperation("ExportWarehouses")]
        [SwaggerResponse(200, type: typeof(Warehouse), description: "Successful response")]
        [SwaggerResponse(400, type: typeof(Error), description: "An error occurred loading.")]
        public virtual IActionResult ExportWarehouses()
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Warehouse));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            IReadOnlyCollection<BusinessLogic.Entities.Warehouse> warehouseList = _warehouseLogic.GetAll();
            
            if(warehouseList.Count <= 0)
                return new BadRequestObjectResult(new Error
                                                  { ErrorMessage = "An error occurred loading." });

            return new ObjectResult(warehouseList) { StatusCode = 200 };
        }

        /// <summary>
        /// Get a certain warehouse or truck by code
        /// </summary>
        /// <param name="code"></param>
        /// <response code="200">Successful response</response>
        /// <response code="400">An error occurred loading.</response>
        /// <response code="404">Warehouse id not found</response>
        [HttpGet]
        [Route("/warehouse/{code}")]
        [ValidateModelState]
        [SwaggerOperation("GetWarehouse")]
        [SwaggerResponse(200, type: typeof(Warehouse), description: "Successful response")]
        [SwaggerResponse(400, type: typeof(Error), description: "An error occurred loading.")]
        [SwaggerResponse(404, type: typeof(Error), description: "Warehouse not found")]
        public virtual IActionResult GetWarehouse([FromRoute] [Required] string code)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Warehouse));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            WarehouseCode warehouseCode = new WarehouseCode(code);

            BusinessLogic.Entities.Warehouse? warehouse = _warehouseLogic.Get(warehouseCode);
            
            if(warehouse is null)
                return new NotFoundObjectResult(new Error
                                                  { ErrorMessage = "Warehouse not found" });

            return new ObjectResult(warehouse) { StatusCode = 200 };
        }

        /// <summary>
        /// Imports a hierarchy of Warehouse and Truck objects.
        /// </summary>
        /// <param name="warehouse"></param>
        /// <response code="200">Successfully loaded.</response>
        /// <response code="400">The operation failed due to an error.</response>
        [HttpPost]
        [Route("/warehouse")]
        [ValidateModelState]
        [SwaggerOperation("ImportWarehouses")]
        [SwaggerResponse(400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult ImportWarehouses([FromBody] Warehouse warehouse)
        {
            BusinessLogic.Entities.Warehouse eWarehouse = _mapper.Map<Warehouse, BusinessLogic.Entities.Warehouse>(warehouse);

            bool wasAdded = _warehouseLogic.Add(eWarehouse);
            
            if(!wasAdded)
                return new BadRequestObjectResult(new Error
                                                  { ErrorMessage = "The operation failed due to an error." });

            return new OkResult();
        }
    }
}