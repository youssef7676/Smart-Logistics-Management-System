using MediatR;
using Smart_Logistics_Mangment_System.Application.AppUsers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Application.RefreshTokenss.Commands
{
    public class RefreshTokenCommand : IRequest<AppUserDTO>
    {
        public string RefreshToken { get; set; }
    }
}
