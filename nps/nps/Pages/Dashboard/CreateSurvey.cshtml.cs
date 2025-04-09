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

    [BindProperty] 
    public SurveyCreationDto SurveyForm { get; set; } = new();

    public List<Question> ExistingQuestions { get; set; } = [];

    public IEnumerable<string> OrderNumbers { get; set; } = [];

    [BindProperty] public string OrderNumber { get; set; }

    public async Task OnGetAsync()
    {
        ExistingQuestions = await _questionService.GetAll();
        OrderNumbers = (await _orderService.GetAllViews())
            .Where(order => !order.HasSurvey)
            .Select(order => order.Number);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _logger.LogInformation("{SurveyForm}", SurveyForm);
        return Page();
    }
    
    
}