﻿using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using NLSL.SKS.Package.Services.Attributes;
using NLSL.SKS.Package.Services.DTOs;

using Swashbuckle.AspNetCore.Annotations;

namespace NLSL.SKS.Package.Services.Controllers
{
    [ApiController]
    public class ParcelWebhookApiController
    {
         /// <summary>
        /// Get all registered subscriptions for the parcel webhook.
        /// </summary>
        /// <param name="trackingId"></param>
        /// <response code="200">List of webooks for the &#x60;trackingId&#x60;</response>
        /// <response code="404">No parcel found with that tracking ID.</response>
        [HttpGet]
        [Route("/parcel/{trackingId}/webhooks")]
        [ValidateModelState]
        [SwaggerOperation("ListParcelWebhooks")]
        [SwaggerResponse(statusCode: 200, type: typeof(WebhookResponses), description: "List of webooks for the &#x60;trackingId&#x60;")]
        public virtual IActionResult ListParcelWebhooks([FromRoute][Required][RegularExpression("/^[A-Z0-9]{9}$/")]string trackingId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(WebhookResponses));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            string exampleJson = null;
            exampleJson = "[ {\n  \"created_at\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : 0,\n  \"url\" : \"url\",\n  \"trackingId\" : \"trackingId\"\n}, {\n  \"created_at\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : 0,\n  \"url\" : \"url\",\n  \"trackingId\" : \"trackingId\"\n} ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<WebhookResponses>(exampleJson)
                        : default(WebhookResponses);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Subscribe to a webhook notification for the specific parcel.
        /// </summary>
        /// <param name="trackingId"></param>
        /// <param name="url"></param>
        /// <response code="200">Successful response</response>
        /// <response code="404">No parcel found with that tracking ID.</response>
        [HttpPost]
        [Route("/parcel/{trackingId}/webhooks")]
        [ValidateModelState]
        [SwaggerOperation("SubscribeParcelWebhook")]
        [SwaggerResponse(statusCode: 200, type: typeof(WebhookResponse), description: "Successful response")]
        public virtual IActionResult SubscribeParcelWebhook([FromRoute][Required][RegularExpression("/^[A-Z0-9]{9}$/")]string trackingId, [FromQuery][Required()]string url)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(WebhookResponse));

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            string exampleJson = null;
            exampleJson = "{\n  \"created_at\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : 0,\n  \"url\" : \"url\",\n  \"trackingId\" : \"trackingId\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<WebhookResponse>(exampleJson)
                        : default(WebhookResponse);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Remove an existing webhook subscription.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Success</response>
        /// <response code="404">Subscription does not exist.</response>
        [HttpDelete]
        [Route("/parcel/webhooks/{id}")]
        [ValidateModelState]
        [SwaggerOperation("UnsubscribeParcelWebhook")]
        public virtual IActionResult UnsubscribeParcelWebhook([FromRoute][Required]long? id)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200);

            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            throw new NotImplementedException();
        }
    }
}