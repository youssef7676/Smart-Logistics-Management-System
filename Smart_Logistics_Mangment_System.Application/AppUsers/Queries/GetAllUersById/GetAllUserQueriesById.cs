using MediatR;
using Smart_Logistics_Mangment_System.Application.AppUsers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.AppUsers.Queries.GetAllUersById
{
    public class GetAllUserQueriesById :IRequest<AppUserDTO>
    {
        public int Id { get; set; }
        public GetAllUserQueriesById(int id)
        {
            Id = id;
        }
    }
}
