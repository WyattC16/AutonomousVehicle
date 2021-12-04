using System.ComponentModel.DataAnnotations;

namespace AutonomousVehicle.Models;

public class SensorsSummary
{
    [Display(Name = "Display Systems Overview")]
    public bool DisplaySystemsOverview { get; set; }

    public string Destination { get; set; } = string.Empty;

    [Display(Name = "Travel Time in Minutes")]
    public int? TravelTimeMinutes { get; set; }

    public int Temperature { get; set; }

    [Display(Name = "Temperature Type")]
    public TemperatureType TemperatureType { get; set; }

    public ICollection<SystemMessage> SystemMessages { get; } = new List<SystemMessage>();

    [Display(Name = "Display Seatbelt Icon")]
    public bool DisplaySeatbeltIcon { get; set; }

    [Display(Name = "Display Check Engine")]
    public bool DisplayCheckEngine { get; set; }

    [Display(Name = "Display Hazards Icon")]
    public bool DisplayHazardsIcon { get; set; }

    [Display(Name = "Display Airbag Icon")]
    public bool DisplayAirbagIcon { get; set; }

    [Display(Name = "Amount of Gas in Tank")]
    public int GasTankAmountGallons { get; set; }

    [Display(Name = "Gas Tank Total Gallons Amount")]
    public int GasTankTotalGallons { get; set; }

    [Display(Name = "Road Condition")]
    public string RoadCondition { get; set; } = string.Empty;

    [Display(Name = "Speed Limit")]
    public int SpeedLimit { get; set; }

    [Display(Name = "Speed Limit")]
    public int Odometer { get; set; }

    [Display(Name = "Next Fuel-up")]
    public int NextFuelUp { get; set; }
}
