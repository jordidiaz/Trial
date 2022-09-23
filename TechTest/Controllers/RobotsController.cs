using Microsoft.AspNetCore.Mvc;
using TechTest.Data;
using TechTest.Domain;
using TechTest.Services;

namespace TechTest.Controllers;

[ApiController]
[Route("[controller]")]
public class RobotsController : ControllerBase
{
    private readonly IEnumerable<INotificationService> _notificationServices;
    private readonly Repository _repository;

    public RobotsController(IEnumerable<INotificationService> notificationServices, Repository repository)
    {
        _notificationServices = notificationServices;
        _repository = repository;
    }

    [HttpGet("required_rooms")]
    public IActionResult GetRequiredRoomsForDay(DateTime date)
    {
        throw new NotImplementedException();
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable(string condition, DateTime date)
    {

        var robotResult = await _repository.GetRobots(x => x.ConditionExpertise == condition);

        robotResult = robotResult.FindAll(r =>
            !r.Appointments.ToList().Exists(a => a.StartDate <= date && date < a.EndDate));
        
        robotResult.ForEach(r => 
        {
            foreach (var notificationService in _notificationServices)
            {
                notificationService.NotifyRobotSelected(r.Id);
            }
        });

        return base.Ok(robotResult.Select(robot => new RobotDto(robot.Id, robot.ConditionExpertise)));
    }
}

public record RobotDto(int Id, string ConditionExpertise);
