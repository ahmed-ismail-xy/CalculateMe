using CalculateMe.Calculation.Api.Models;
using CalculateMe.Calculation.Application.Services;
using CalculateMe.Calculation.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CalculateMe.Calculation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculationsController : ControllerBase
{
    private readonly ICalculationService _calculationService;

    public CalculationsController(ICalculationService calculationService)
    {
        _calculationService = calculationService;
    }

    [HttpPost]
    public async Task<IActionResult> Calculate([FromBody] CalculationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var calculation = await _calculationService.CalculateAsync(
            request.FirstNumber,
            request.SecondNumber);

        return AcceptedAtAction(
            nameof(GetCalculation),
            new { id = calculation.Id },
            new { id = calculation.Id, status = SummationTaskStatus.Pending });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCalculation(Guid id)
    {
        var calculation = await _calculationService.GetCalculationAsync(id);

        if (calculation == null)
            return NotFound();

        return Ok(new CalculationResponse
        (
            calculation.Id,
            calculation.FirstNumber,
            calculation.SecondNumber,
            calculation.Result,
            calculation.Status.ToString(),
            calculation.CreatedAt,
            calculation.CompletedAt));
    }
}
