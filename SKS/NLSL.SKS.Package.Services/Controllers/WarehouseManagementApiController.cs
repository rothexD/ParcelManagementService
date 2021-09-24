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

using Microsoft.AspNetCore.Mvc;

using NLSL.SKS.Package.Services.Attributes;
using NLSL.SKS.Package.Services.DTOs;

using Swashbuckle.AspNetCore.Annotations;

namespace NLSL.SKS.Package.Services.Controllers
{
    /// <summary>
    /// </summary>
    [ApiController]
    public class WarehouseManagementApiController : ControllerBase
    {
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

            Warehouse warehouse = new Warehouse
                                  {
                                      Code = "code12345"
                                  };

            List<Warehouse> warehouseList = new List<Warehouse>
                                            { warehouse };


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
        public virtual IActionResult GetWarehouse([FromRoute] [Required] string code)
        {
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Warehouse));

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            if (code != "test")
                return new NotFoundObjectResult(null);

            Warehouse warehouse = new Warehouse
                                  {
                                      Code = "code12345"
                                  };

            return new ObjectResult(warehouse) { StatusCode = 200 };
        }

        /// <summary>
        /// Imports a hierarchy of Warehouse and Truck objects.
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">Successfully loaded.</response>
        /// <response code="400">The operation failed due to an error.</response>
        [HttpPost]
        [Route("/warehouse")]
        [ValidateModelState]
        [SwaggerOperation("ImportWarehouses")]
        [SwaggerResponse(400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult ImportWarehouses([FromBody] Warehouse body)
        {
            if (body == null)
                return StatusCode(400);


            return StatusCode(200);
        }
    }
}