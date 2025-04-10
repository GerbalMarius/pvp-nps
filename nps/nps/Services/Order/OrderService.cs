using Microsoft.EntityFrameworkCore;
using nps.Migrations.Data;
using nps.Models.DTOS;

namespace nps.Services.Order;

public sealed class OrderService : IOrderService
{
    private readonly AppDbContext _db;

    private readonly ILogger<OrderService> _logger;

    public OrderService(AppDbContext db, ILogger<OrderService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<Models.Order?> GetOrderByNumber(string number)
    {
        return await _db.Orders.SingleOrDefaultAsync(order => order.Number == number);
    }

    public async Task<List<OrderView>> GetAllViews()
    {
        return await _db.Orders.OrderBy(order => order.Id)
            .Select(
                order => new OrderView(order.Id, order.Number, order.OrderDate, order.DeliveryDate, order.ClientEmail,
                    order.HasSurvey)
            ).ToListAsync();
    }
}