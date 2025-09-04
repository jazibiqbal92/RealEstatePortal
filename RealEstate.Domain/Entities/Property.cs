using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain.Enums;

namespace RealEstate.Domain.Entities
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(300)]
        public string Address { get; set; }

        [Range(0, int.MaxValue)]
        public int? BedroomCount { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int? BathroomCount { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int? CarspotCount { get; set; } = 0;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        public ListingType ListingType { get; set; }

        public ICollection<Favorite>? Favorites { get; set; }
    }
}
