using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.DTOs
{
    public class FavoriteCreateDto
    {
        public int UserId { get; set; }
        public int PropertyId { get; set; }
    }
}
