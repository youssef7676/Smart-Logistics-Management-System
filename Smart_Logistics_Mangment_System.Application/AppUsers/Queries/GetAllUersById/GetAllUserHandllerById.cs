using AutoMapper;
using MediatR;
using Smart_Logistics_Mangment_System.Application.AppUsers.DTOs;
using Smart_Logistics_Mangment_System.Application.Customers.DTOs;
using Smart_Logistics_Mangment_System.Domain.Interfaces;
using Smart_Logistics_Mangment_System.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.AppUsers.Queries.GetAllUersById
{
    public class GetAllUserHandllerById(IAppUserReposatory userrepo, IMapper mapper)
        : IRequestHandler<GetAllUserQueriesById, AppUserDTO>
    {
        public async Task<AppUserDTO> Handle(GetAllUserQueriesById request, CancellationToken cancellationToken)
        {
            AppUser users = await userrepo.GetById(request.Id);

            if (users == null)
                return null;

            return mapper.Map<AppUserDTO>(users);
        }
    }
}
