using System.ComponentModel.DataAnnotations;

namespace ShopApi.DTOs.Request
{
    public class AuthorRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Status { get; set; }
    }
}
