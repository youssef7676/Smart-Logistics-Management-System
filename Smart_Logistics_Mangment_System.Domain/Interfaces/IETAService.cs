using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Domain.Interfaces
{
    public interface IETAService
    {
        double CalculateDistanceKm(
            decimal latitude1,
            decimal longitude1,
            decimal latitude2,
            decimal longitude2);

        int CalculateEstimatedMinutes(
            double distanceKm);
    }
}
