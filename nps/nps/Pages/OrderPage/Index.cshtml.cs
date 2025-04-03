using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using nps.Migrations.Data;
using nps.Models;

namespace nps.Pages.OrderPage;

public class Index : PageModel
{
    private readonly ILogger<Index> _logger;
    
    private readonly AppDbContext _db;

    public IEnumerable<Order> OrderData { get; set; }


    public Index(AppDbContext db, ILogger<Index> logger)
    {
        _logger = logger;
        _db = db;
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        var actual = await _db.Orders.ToListAsync();
            

        OrderData = actual;
        return Page();
    }
}