/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.20.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.ComponentModel.DataAnnotations;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using NLSL.SKS.Package.BusinessLogic.Interfaces;
using NLSL.SKS.Package.Services.Attributes;
using NLSL.SKS.Package.Services.DTOs;

using Swashbuckle.AspNetCore.Annotations;

namespace NLSL.SKS.Package.Services.Controllers
{
    /// <summary>
    /// </summary>
    [ApiController]
    public class LogisticsPartnerApiController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IParcelLogic _parcelLogic;
        public LogisticsPartnerApiController(IParcelLogic parcelLogic, IMapper mapper)
        {
            _parcelLogic = parcelLogic;
            _mapper = mapper;
        }
        /// <summary>
        /// Transfer an existing parcel into the system from the service of a logistics partner.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="trackingId">The tracking ID of the parcel. E.g. PYJRB4HZ6 </param>
        /// <response code="200">Successfully transitioned the parcel</response>
        /// <response code="400">The operation failed due to an error.</response>
        [HttpPost]
        [Route("/parcel/{trackingId}")]
        [ValidateModelState]
        [SwaggerOperation("TransitionParcel")]
        [SwaggerResponse(200, type: typeof(NewParcelInfo), description: "Successfully transitioned the parcel")]
        [SwaggerResponse(400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult TransitionParcel([FromBody] Parcel parcel, [FromRoute] [Required] [RegularExpression("^[A-Z0-9]{9}$")] string trackingId)
        {
            try
            {
                //automapper 
                BusinessLogic.Entities.Parcel eParcel = _mapper.Map<Parcel, BusinessLogic.Entities.Parcel>(parcel);
                eParcel.TrackingId = trackingId;

                BusinessLogic.Entities.Parcel? transitionResult = _parcelLogic.Submit(eParcel);

                if (transitionResult is null)
                    return new BadRequestObjectResult(new Error
                                                      { ErrorMessage = "The operation failed due to an error." });

                NewParcelInfo? newParcel = _mapper.Map<BusinessLogic.Entities.Parcel, NewParcelInfo>(transitionResult);

                return new OkObjectResult(newParcel);
            }
            catch (Exception exception)
            {
                return new BadRequestObjectResult(new Error
                                                  { ErrorMessage = $"The operation failed due to an error. {exception.Message}" });
            }
         
        }
    }
}