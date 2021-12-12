namespace AutonomousVehicle.Models;

public record SystemMessage
{
    public SystemMessageCriticality MessageCriticality { get; set; }

    public string Message { get; set; } = string.Empty;

    public int Order { get; set; }
}
