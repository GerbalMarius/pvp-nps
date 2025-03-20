using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using nps.Migrations.Data;
using nps.Models;

namespace nps.Pages.OrderPage;

public class OrderPage : PageModel
{
    private readonly ILogger<OrderPage> _logger;
    
    private readonly AppDbContext _db;

    public UserDto? User { get; set; }

    public OrderPage(AppDbContext db, ILogger<OrderPage> logger)
    {
        _logger = logger;
        _db = db;
    }
    
    public async Task<IActionResult> OnGet()
    {
        var actual = await _db.Users.FindAsync(1L);
        
        User = new UserDto{Email = actual.Email, TelephoneNumber = actual.TelephoneNumber, Id = actual.Id};
        return Page();
    }
}