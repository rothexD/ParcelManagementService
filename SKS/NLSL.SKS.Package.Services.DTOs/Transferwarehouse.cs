/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.20.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace NLSL.SKS.Package.Services.DTOs
{
    /// <summary>
    /// </summary>
    [DataContract]
    [ExcludeFromCodeCoverage]
    public class Transferwarehouse : Hop
    {
        /// <summary>
        /// GeoJSON of the are covered by the logistics partner.
        /// </summary>
        /// <value>GeoJSON of the are covered by the logistics partner.</value>
        [Required]
        [DataMember(Name = "regionGeoJson")]
        public string RegionGeoJson
        {
            get;
            set;
        }

        /// <summary>
        /// Name of the logistics partner.
        /// </summary>
        /// <value>Name of the logistics partner.</value>
        [Required]
        [DataMember(Name = "logisticsPartner")]
        public string LogisticsPartner
        {
            get;
            set;
        }

        /// <summary>
        /// BaseURL of the logistics partner&#x27;s REST service.
        /// </summary>
        /// <value>BaseURL of the logistics partner&#x27;s REST service.</value>
        [Required]
        [DataMember(Name = "logisticsPartnerUrl")]
        public string LogisticsPartnerUrl
        {
            get;
            set;
        }
    }
}