namespace TechTest.Services;

public interface INotificationService
{
    public void NotifyRobotSelected(int robotId);
}

public class EngineeringNotificationService : INotificationService
{
    public void NotifyRobotSelected(int robotId)
    {
        //For exercise purposes imagine there is some relevant code here
    }
}

public class CustomerNotificationService : INotificationService
{
    public void NotifyRobotSelected(int robotId)
    {
        //For exercise purposes imagine there is some relevant code here
    }
}

public class InvoicingNotificationService : INotificationService
{
    public void NotifyRobotSelected(int robotId)
    {
        //For exercise purposes imagine there is some relevant code here
    }
}
