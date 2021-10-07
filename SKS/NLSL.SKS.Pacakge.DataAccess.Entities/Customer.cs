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
using System.Net.Sockets;

namespace NLSL.SKS.Pacakge.DataAccess.Entities
{
    /// <summary>
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Customer
    {
        public int Id
        {
            get;
            set;
        }
        
        /// <summary>
        /// Name of person or company.
        /// </summary>
        /// <value>Name of person or company.</value>
        public string Name
        {
            get;
            set;
        }

        public Address Address
        {
            get;
            set;
        }
    }
}