using RealEstate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.DTOs
{
    public class FavoriteDto
    {
        public int UserId { get; set; }

        public int PropertyId { get; set; }

        public string? UserEmail { get; set; }   
        public string? PropertyTitle { get; set; }   
        public decimal? PropertyPrice { get; set; }
        public string? PropertyListingType { get; set; }
        public string? PropertyImageUrl { get; set; }
    }
}
