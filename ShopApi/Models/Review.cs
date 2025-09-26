using Microsoft.EntityFrameworkCore;

namespace ShopApi.Models
{
    [PrimaryKey(nameof(ApplicationUserId), nameof(ProductId))]
    public class Review
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        //rate and reviews
        public int Rate { get; set; }
        public string? Note { get; set; } = string.Empty;
    }
}
