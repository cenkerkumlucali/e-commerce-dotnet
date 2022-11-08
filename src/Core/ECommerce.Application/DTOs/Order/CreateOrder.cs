namespace ECommerce.Application.DTOs.Order;

public class CreateOrder
{
    public string? BasketId { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
}