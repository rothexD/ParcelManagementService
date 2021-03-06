/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.20.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System.Diagnostics.CodeAnalysis;

using NetTopologySuite.Geometries;

namespace NLSL.SKS.Package.DataAccess.Entities
{
    /// <summary>
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GeoCoordinate
    {
        public int Id
        {
            get;
            set;
        }
        
       public Point Location
        {
            get;
            set;
        }
        
    }
}