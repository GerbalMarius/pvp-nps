using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using nps.Migrations.Data;
using nps.Models.DTOS;
using nps.Services.Order;

namespace nps.Pages.Dashboard;

public class Orders : PageModel
{
    private readonly ILogger<Orders> _logger;
    private readonly IOrderService _orderService;

    public List<OrderView> OrderData { get; set; } = [];

    public Orders(ILogger<Orders> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        OrderData = await _orderService.GetAllViews();
        return Page();
    }
}