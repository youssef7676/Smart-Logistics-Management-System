using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics_Mangment_System.Domain.Models
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }


        public DateTime ExpiryDate { get; set; }


        public bool IsRevoked { get; set; }


        public DateTime? RevokedAt { get; set; }



        // User

        public int UserId { get; set; }

        public AppUser User { get; set; }
    }
}
