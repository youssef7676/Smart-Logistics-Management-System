using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Domain.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get;  }
        string Role { get; }


    }
}
