using System.ComponentModel.DataAnnotations;

namespace ShopApi.DTOs.Request
{
    public class ResendEmailConfirmationDTO
    {
        [Required]
        public string EmailOrUserName { get; set; } = string.Empty;
    }
}
