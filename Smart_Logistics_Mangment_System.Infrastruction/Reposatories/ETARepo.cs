using Smart_Logistics_Mangment_System.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Infrastruction.Reposatories
{

        public class ETARepo : IETAService
        {
            private const double AverageSpeedKmPerHour = 40;

            public double CalculateDistanceKm(
                decimal latitude1,
                decimal longitude1,
                decimal latitude2,
                decimal longitude2)
            {
                const double earthRadiusKm = 6371;

                double lat1 = ToRadians((double)latitude1);
                double lat2 = ToRadians((double)latitude2);

                double deltaLat =
                    ToRadians(
                        (double)(latitude2 - latitude1));

                double deltaLon =
                    ToRadians(
                        (double)(longitude2 - longitude1));

                double a =
                    Math.Sin(deltaLat / 2) *
                    Math.Sin(deltaLat / 2)
                    +
                    Math.Cos(lat1) *
                    Math.Cos(lat2) *
                    Math.Sin(deltaLon / 2) *
                    Math.Sin(deltaLon / 2);

                double c =
                    2 *
                    Math.Atan2(
                        Math.Sqrt(a),
                        Math.Sqrt(1 - a));

                return earthRadiusKm * c;
            }

            public int CalculateEstimatedMinutes(
                double distanceKm)
            {
                if (distanceKm <= 0)
                    return 0;

                double hours =
                    distanceKm / AverageSpeedKmPerHour;

                return (int)Math.Ceiling(hours * 60);
            }

            private static double ToRadians(double value)
            {
                return value * Math.PI / 180;
            }
        }
}
