/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.20.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using NLSL.SKS.Package.Services.DTOs.Enums;
using System.Diagnostics.CodeAnalysis;

namespace NLSL.SKS.Package.Services.DTOs
{
    /// <summary>
    /// </summary>
    [DataContract]
    [ExcludeFromCodeCoverage]
    public class TrackingInformation
    {
        /// <summary>
        /// State of the parcel.
        /// </summary>
        /// <value>State of the parcel.</value>
        [Required]
        [DataMember(Name = "state")]
        public StateEnum? State
        {
            get;
            set;
        }

        /// <summary>
        /// Hops visited in the past.
        /// </summary>
        /// <value>Hops visited in the past.</value>
        [Required]
        [DataMember(Name = "visitedHops")]
        public List<HopArrival> VisitedHops
        {
            get;
            set;
        } = new List<HopArrival>();

        /// <summary>
        /// Hops coming up in the future - their times are estimations.
        /// </summary>
        /// <value>Hops coming up in the future - their times are estimations.</value>
        [Required]
        [DataMember(Name = "futureHops")]
        public List<HopArrival> FutureHops
        {
            get;
            set;
        } = new List<HopArrival>();
    }
}