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
using System.Diagnostics.CodeAnalysis;

using NLSL.SKS.Package.DataAccess.Entities.Enums;

namespace NLSL.SKS.Package.DataAccess.Entities
{
    /// <summary>
    /// </summary>
    [ExcludeFromCodeCoverage]

    public class Parcel
    {
        public int Id
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or Sets Weight
        /// </summary>
        public float? Weight
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets Recipient
        /// </summary>
        public Recipient Recipient
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets Sender
        /// </summary>
        public Recipient Sender
        {
            get;
            set;
        }
        /// <summary>
        /// State of the parcel.
        /// </summary>
        /// <value>State of the parcel.</value>
        public StateEnum? State
        {
            get;
            set;
        }

        /// <summary>
        /// Hops visited in the past.
        /// </summary>
        /// <value>Hops visited in the past.</value>
        public List<HopArrival> VisitedHops
        {
            get;
            set;
        }

        /// <summary>
        /// Hops coming up in the future - their times are estimations.
        /// </summary>
        /// <value>Hops coming up in the future - their times are estimations.</value>
        public List<HopArrival> FutureHops
        {
            get;
            set;
        }
        
        public string TrackingId
        {
            get;
            set;
        }
    }
}