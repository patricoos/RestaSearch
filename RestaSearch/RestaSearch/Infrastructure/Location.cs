using RestaSearch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klubowo.Core.Models.Locations
{
    public class Location
    {
        #region Private fields

        private double _latitude;
        private double _longitude;

        #endregion

        #region Ctors

        public Location()
        {
        }

        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        #endregion

        #region Properties

        public static double WorldRadiusInMeters => 6371000;

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                if (value > 180 || value < -180)
                    throw new Exception("Invalid latitude");
                _latitude = value;
            }
        }

        public double Longitude
        {
            get { return _longitude; }
            set
            {
                if (value > 180 || value < -180)
                    throw new Exception("Invalid longitude");
                _longitude = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the distance (in meters) between locations.
        /// </summary>
        /// <param name="otherLocation"></param>
        /// <returns></returns>
        public double GetDistance(Location otherLocation)
        {
            //Haversine
            //formula:	a = sin²(Δφ / 2) + cos φ1 ⋅ cos φ2 ⋅ sin²(Δλ / 2)
            //c = 2 ⋅ atan2( √a, √(1−a) )
            //d = R ⋅ c
            double worldRadiusInMeters = WorldRadiusInMeters;
            var thisLatRadians = Latitude.ToRadians();
            var otherLatRadians = otherLocation.Latitude.ToRadians();
            var latDiffInRadians = (otherLocation.Latitude - Latitude).ToRadians();
            var lonDiffInRadians = (otherLocation.Longitude - Longitude).ToRadians();

            var a = Math.Pow(Math.Sin(latDiffInRadians / 2), 2) +
                    Math.Cos(thisLatRadians) * Math.Cos(otherLatRadians) *
                    Math.Pow(Math.Sin(lonDiffInRadians / 2), 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = worldRadiusInMeters * c;

            return distance;
        }

        /// <summary>
        /// Return the bounds, where the current lcoation is the center of this location
        /// and with a side size as the given parameter.
        /// </summary>
        /// <param name="sideSizeInMeters"></param>
        /// <returns></returns>
        //public MapBounds GetMapBounds(double sideSizeInMeters)
        //{
        //    var north = Latitude + (sideSizeInMeters / GetMetersPerDegreeOfLatitude());
        //    var west = Longitude - (sideSizeInMeters / GetMetersPerDegreeOfLongitude(north));

        //    var south = Latitude - (sideSizeInMeters / GetMetersPerDegreeOfLatitude());
        //    var east = Longitude + (sideSizeInMeters / GetMetersPerDegreeOfLongitude(south));

        //    return new MapBounds {North = north, West = west, South = south, East = east};
        //}

        public static double GetMetersPerDegreeOfLongitude(double latitude)
        {
            return (Math.PI / 180) * WorldRadiusInMeters * Math.Cos(latitude.ToRadians());
        }

        public static double GetMetersPerDegreeOfLatitude()
        {
            return (Math.PI / 180) * WorldRadiusInMeters;
        }

        #endregion

        #region Overrides

        public override int GetHashCode()
        {
            unchecked
            {
                return (Latitude.GetHashCode() * 397) ^ Longitude.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"({Latitude}, {Longitude})";
        }

        protected bool Equals(Location other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Location) obj);
        }

        public static bool operator ==(Location loc1, Location loc2)
        {
            if (ReferenceEquals(loc1, null) && ReferenceEquals(loc2, null)) return true;
            if (ReferenceEquals(loc1, null)) return false;
            if (ReferenceEquals(loc2, null)) return false;

            return (Math.Abs(loc1.Latitude - loc2.Latitude) < 0.000001 &&
                    Math.Abs(loc1.Longitude - loc2.Longitude) < 0.000001);
        }

        public static bool operator !=(Location loc1, Location loc2)
        {
            return !(loc1 == loc2);
        }

        #endregion
    }
}