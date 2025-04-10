using nps.Models.DTOS;

namespace nps.Services.Order;

public interface IOrderService
{
    Task<Models.Order?> GetOrderByNumber(string number);
    
    Task<List<OrderView>> GetAllViews();
}