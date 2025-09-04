using RealEstate.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.DTOs
{
    public class PropertyCreateDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Address { get; set; }

        public int? BedroomCount { get; set; }

        public int? BathroomCount { get; set; }

        public int? CarspotCount { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
        public ListingType ListingType { get; set; }
    }
}
