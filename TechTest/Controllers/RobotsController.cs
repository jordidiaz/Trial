using Microsoft.AspNetCore.Mvc;
using TechTest.Data;
using TechTest.Domain;
using TechTest.Services;

namespace TechTest.Controllers;

[ApiController]
[Route("[controller]")]
public class RobotsController : ControllerBase
{
    private readonly DataContext _context;

    public RobotsController(DataContext context)
    {
        this._context = context;
    }

    [HttpGet("required_rooms")]
    public IActionResult GetRequiredRoomsForDay(DateTime date)
    {
        throw new NotImplementedException();
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable(string condition)
    {
        var repository = new Repository(_context);

        var robotResult = await repository.GetRobots(x => x.ConditionExpertise == condition);
        
        robotResult.ForEach(r => 
        {
            new EngineeringNotificationService().NotifyRobotSelected(r.Id);
            new CustomerNotificationService().NotifyRobotSelected(r.Id);
            new InvoicingNotificationService().NotifyRobotSelected(r.Id);
        });

        var response = new List<object>();
        for (int j = 0; j < robotResult.Count; j++)
        {
            response.Add(new { id = robotResult[j].Id, conditionExpertise = robotResult[j].ConditionExpertise });
        }

        return base.Ok(response);
    }
}
