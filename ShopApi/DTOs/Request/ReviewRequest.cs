namespace ShopApi.DTOs.Request;

public class ReviewRequest
{
    public int ProductId { get; set; }
    public int Rate { get; set; }
    
    public string Note { get; set; } = string.Empty;
}