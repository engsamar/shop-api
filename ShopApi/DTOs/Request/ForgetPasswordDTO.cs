using System.ComponentModel.DataAnnotations;

namespace ShopApi.DTOs.Request
{
    public class ForgetPasswordDTO
    {
        [Required]
        public string EmailOrUserName { get; set; } = string.Empty;
    }
}
