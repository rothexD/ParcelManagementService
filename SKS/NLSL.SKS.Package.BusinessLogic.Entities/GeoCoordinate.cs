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

namespace NLSL.SKS.Package.BusinessLogic.Entities
{
    /// <summary>
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GeoCoordinate
    {
        /// <summary>
        /// Latitude of the coordinate.
        /// </summary>
        /// <value>Latitude of the coordinate.</value>
        public double? Lat
        {
            get;
            set;
        }

        /// <summary>
        /// Longitude of the coordinate.
        /// </summary>
        /// <value>Longitude of the coordinate.</value>
        public double? Lon
        {
            get;
            set;
        }
    }
}