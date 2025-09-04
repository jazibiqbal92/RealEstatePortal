using RealEstate.Domain.Enums;

namespace RealEstate.Application.DTOs
{
    public class PropertyDto
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
        public string ListingType { get; set; }
    }
}
