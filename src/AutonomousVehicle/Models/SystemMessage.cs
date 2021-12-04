namespace AutonomousVehicle.Models;

public class SystemMessage
{
    public SystemMessageCriticality MessageCriticality { get; set; }

    public string Message { get; set; } = string.Empty;
}
