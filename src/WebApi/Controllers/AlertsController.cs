using Common.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;

namespace WebApi.Controllers;

[ApiController]
[Route("alerts")]
public class AlertsController : Controller
{
    private readonly IRepository<Alert> _alertRepository;

    public AlertsController(IRepository<Alert> alertRepository)
    {
        _alertRepository = alertRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetAsync()
    {
        var result = await _alertRepository.GetAllAsync();
        return Ok(result);
    }

}