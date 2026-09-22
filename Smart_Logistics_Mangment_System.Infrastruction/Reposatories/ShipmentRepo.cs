using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using Smart_Logistics_Mangment_System.Infrastruction.DB_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Infrastruction.Reposatories
{
    public class ShipmentRepo : Reposatory<Shipment>, IShipmentReposatory
    {
        public ShipmentRepo(Application_Context context) : base(context)
        {

        }

    }
    
}
