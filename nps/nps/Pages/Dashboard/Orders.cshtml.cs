using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using nps.Migrations.Data;
using nps.Models.DTOS;

namespace nps.Pages.Dashboard;

public class Orders : PageModel
{
    private readonly ILogger<Orders> _logger;
    private readonly AppDbContext _dbContext;

    public List<OrderView> OrderData { get; set; } = [];

    public Orders(ILogger<Orders> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        OrderData = await _dbContext.Orders.OrderBy(order => order.Id)
            .Select(order => 
                new OrderView(order.Id,order.Number, order.OrderDate, order.DeliveryDate ,order.ClientEmail, order.HasSurvey)
            )
            .ToListAsync();
        return Page();
    }
}