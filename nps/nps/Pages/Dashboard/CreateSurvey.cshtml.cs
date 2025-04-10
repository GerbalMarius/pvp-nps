using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nps.Models.DTOS;
using nps.Models.SurveyQuestions;
using nps.Services.Order;
using nps.Services.Question;
using nps.Services.Survey;

namespace nps.Pages.Dashboard;

public class CreateSurvey : PageModel
{
    private readonly ILogger<CreateSurvey> _logger;
    private readonly IOrderService _orderService;
    private readonly IQuestionService _questionService;
    private readonly ISurveyService _surveyService;

    public CreateSurvey(IQuestionService questionService, ISurveyService surveyService, IOrderService orderService,
        ILogger<CreateSurvey> logger)
    {
        _questionService = questionService;
        _surveyService = surveyService;
        _logger = logger;
        _orderService = orderService;
    }

    [BindProperty] public SurveyCreationDto SurveyForm { get; set; } = new();

    public List<Question> ExistingQuestions { get; set; } = [];

    public IReadOnlyList<string> OrderNumbers { get; set; } = [];

    public IReadOnlyList<SurveyView> AvailableSurveys { get; set; } = [];

    [BindProperty]
    [Required(ErrorMessage = "Order number must be selected")]
    public string OrderNumber { get; set; }
    
    [BindProperty]
    public long? SurveyId { get; set; }

    public async Task OnGetAsync()
    {
        await LoadData();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await LoadData();
        
        _logger.LogInformation("{survey}", SurveyForm);

        if (SurveyId.HasValue)
        {
            await _surveyService.AssignSurveyForOrder(SurveyId.Value, OrderNumber);
        }
        else
        {
            await _surveyService.CreateSurveyForOrder(SurveyForm, OrderNumber);
        }
        
        return RedirectToPage("CreateSurvey", new { OrderNumber, success = true });
    }

    private async Task LoadData()
    {
        OrderNumbers = (await _orderService.GetAllViews())
            .Where(order => !order.HasSurvey)
            .Select(order => order.Number).ToList();

        ExistingQuestions = await _questionService.GetAll();
        
        AvailableSurveys = await _surveyService.GetAll();
    }
}