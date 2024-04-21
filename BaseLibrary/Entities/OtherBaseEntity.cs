using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class OtherBaseEntity
    {
        public int Id { get; set; } 

        [Required]
        public string? CivilId { get; set; }

        [Required]
        public string? FieNumber { get; set; }
        public string? Other { get; set; }
    }
}