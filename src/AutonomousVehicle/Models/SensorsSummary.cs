using System.ComponentModel.DataAnnotations;

namespace AutonomousVehicle.Models;

public class SensorsSummary
{
    [Display(Name = "Display Systems Overview")]
    public bool DisplaySystemsOverview { get; set; }

    [Display(Name = "Destination")]
    public string Destination { get; set; } = string.Empty;

    [Display(Name = "Travel Time in Minutes")]
    public int? TravelTimeMinutes { get; set; }

    [Display(Name = "Temperature")]
    public int Temperature { get; set; }

    [Display(Name = "Temperature Type")]
    public TemperatureType TemperatureType { get; set; }

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

    [Display(Name = "Odometer")]
    public int Odometer { get; set; }

    [Display(Name = "Next Fuel-up")]
    public int NextFuelUp { get; set; }

    [Display(Name = "Tire Pressure Condition")]
    public SystemStatusCondition TirePressureCondition { get; set; }

    [Display(Name = "Oil Pressure Condition")]
    public SystemStatusCondition OilPressureCondition { get; set; }

    [Display(Name = "Temperature Condition")]
    public SystemStatusCondition TemperatureCondition { get; set; }

    [Display(Name = "Battery Condition")]
    public SystemStatusCondition BatteryCondition { get; set; }

    [Display(Name = "Power Steering Condition")]
    public SystemStatusCondition PowerSteeringCondition { get; set; }

    [Display(Name = "Engine Condition")]
    public SystemStatusCondition EngineCondition { get; set; }

    [Display(Name = "Sensors Condition")]
    public SystemStatusCondition SensorsCondition { get; set; }

    [Display(Name = "Processor Condition")]
    public SystemStatusCondition ProcessorCondition { get; set; }

    [Display(Name = "Current Speed")]
    public int CurrentSpeed { get; set; }

    [Display(Name = "Speed Change")]
    public SpeedChange SpeedChange { get; set; }

    [Display(Name = "Signal Direction")]
    public SignalDirection SignalDirection { get; set; }

    [Display(Name = "Gear")]
    public Gear Gear { get; set; }

    [Display(Name = "Engine RPMS")]
    public int EngineRpms { get; set; }

    [Display(Name = "Air Intake")]
    public int AirIntake { get; set; }

    [Display(Name = "Coolant")]
    public int Coolant { get; set; }

    [Display(Name = "Battery Voltage")]
    public decimal BatteryVoltage { get; set; }

    [Display(Name = "Front Driver Side Tire PSI")]
    public int FrontDriverSideTirePsi { get; set; }

    [Display(Name = "Front Passenger Side Tire PSI")]
    public int FrontPassengerSideTirePsi { get; set; }

    [Display(Name = "Back Driver Side Tire PSI")]
    public int BackDriverSideTirePsi { get; set; }

    [Display(Name = "Back Passenger Side Tire PSI")]
    public int BackPassengerSideTirePsi { get; set; }

    [Display(Name = "Engine Oil Life")]
    public int EngineOilLife { get; set; }

    [Display(Name = "Washer Fluid Level")]
    public string WasherFluidLevel { get; set; } = string.Empty;

    [Display(Name = "Brake Life")]
    public int BrakeLife { get; set; }

    public ICollection<SystemMessage> SystemMessages { get; } = new List<SystemMessage>();
}
