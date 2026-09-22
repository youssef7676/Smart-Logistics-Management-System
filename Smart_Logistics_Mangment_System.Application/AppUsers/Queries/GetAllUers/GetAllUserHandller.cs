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

namespace Smart_Logistics_Mangment_System.Application.AppUsers.Queries.GetAllUers
{
    public class GetAllUserHandller(IAppUserReposatory userrepo, IMapper mapper)
        : IRequestHandler<GetAllUserQueries, List<AppUserDTO>>
    {
        public async Task<List<AppUserDTO>> Handle(GetAllUserQueries request, CancellationToken cancellationToken)
        {
            List<AppUser> users = await userrepo.GetAll();
            List<AppUserDTO> userdto = mapper.Map<List<AppUserDTO>>(users);
            return userdto;
        }
    }
}
