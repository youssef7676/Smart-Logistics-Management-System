using MediatR;
using Smart_Logistics_Mangment_System.Application.AppUsers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.AppUsers.Queries.GetAllUers
{
    public class GetAllUserQueries : IRequest<List<AppUserDTO>>
    {
    }
}
