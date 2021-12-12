using AutonomousVehicle.Models;

namespace AutonomousVehicle;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA5394:Do not use insecure randomness", Justification = "This is not security critical")]
public class SensorsRandomizer
{
    private readonly SensorsSummary _sensorsSummary;
    private readonly Action _onRandomize;
    private readonly SystemMessagesSpeechSynthesis _speechSynthesis;
    private readonly Random _random;

    public SensorsRandomizer(SensorsSummary sensorsSummary, Action onRandomize, SystemMessagesSpeechSynthesis speechSynthesis)
    {
        _sensorsSummary = sensorsSummary;
        _onRandomize = onRandomize;
        _speechSynthesis = speechSynthesis;
        _random = new Random();
    }

    public async Task RandomizeAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            if (_sensorsSummary.RandomizeValues)
            {
                Randomize();
            }

            try
            {
                await Task.Delay(2000, cancellationToken).ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
                return;
            }
        }
    }

    private void Randomize()
    {
        var applyUpdates = _random.Next(30) == 0;
        if (applyUpdates)
        {
            _sensorsSummary.DisplaySystemsOverview = !_sensorsSummary.DisplaySystemsOverview;
        }

        if (_sensorsSummary.DisplaySystemsOverview)
        {
            applyUpdates |= RandomizeSystemsOverview();
        }
        else
        {
            applyUpdates |= RandomizeDestination();
            applyUpdates |= RandomizeTemperature();
            applyUpdates |= RandomizeEmergencyIcons();
            applyUpdates |= RandomizeGas();
            applyUpdates |= RandomizeSystemStatuses();
            applyUpdates |= RandomizeCurrentSpeed();
            applyUpdates |= RandomizeSystemMessages();
        }

        if (applyUpdates)
            _onRandomize();
    }

    private bool RandomizeDestination()
    {
        var applyUpdates = string.IsNullOrWhiteSpace(_sensorsSummary.Destination) || !_sensorsSummary.TravelTimeMinutes.HasValue || _random.Next(60) == 0;
        if (applyUpdates)
        {
            _sensorsSummary.Destination = RandomArrayValue(new List<string>
            {
                "Walmart, 900 Stillater Ave, Bangor",
                "McDonalds, 441 Main St, Bangor",
                "McDonalds, 60 Longview Dr"
            });
            _sensorsSummary.TravelTimeMinutes = _random.Next(1, 60);
        }
        return applyUpdates;
    }

    private bool RandomizeTemperature()
    {
        var applyUpdates = _sensorsSummary.TemperatureType == TemperatureType.None || _random.Next(60) == 0;
        if (applyUpdates)
        {
            _sensorsSummary.Temperature = _random.Next(60);
            _sensorsSummary.TemperatureType = RandomArrayValue(Enum.GetValues<TemperatureType>().Skip(1).ToList());
        }
        return applyUpdates;
    }

    private bool RandomizeEmergencyIcons()
    {
        var applyUpdates = _random.Next(60) == 0;
        if (applyUpdates)
        {
            _sensorsSummary.DisplaySeatbeltIcon = RandomBool();
            _sensorsSummary.DisplayCheckEngine = RandomBool();
            _sensorsSummary.DisplayHazardsIcon = RandomBool();
            _sensorsSummary.DisplayAirbagIcon = RandomBool();
        }
        return applyUpdates;
    }

    private bool RandomizeGas()
    {
        var applyUpdates = _sensorsSummary.GasTankTotalGallons == 0 || _random.Next(60) == 0;
        if (applyUpdates)
        {
            _sensorsSummary.GasTankTotalGallons = _sensorsSummary.GasTankTotalGallons == 0 ? _random.Next(10, 21) : _sensorsSummary.GasTankTotalGallons;
            _sensorsSummary.GasTankAmountGallons -= _sensorsSummary.GasTankAmountGallons == 0 ? _sensorsSummary.GasTankTotalGallons * -1 : _random.Next(2);
        }
        return applyUpdates;
    }

    private bool RandomizeSystemStatuses()
    {
        var applyRoadConditionChange = string.IsNullOrWhiteSpace(_sensorsSummary.RoadCondition) ||
            _random.Next(60) == 0;

        if (applyRoadConditionChange)
        {
            _sensorsSummary.RoadCondition = RandomArrayValue(new List<string>
            {
                "Normal, Dry",
                "Normal, Wet",
                "Slippery, Snowing"
            });
        }

        var applySpeedLimitChange = _sensorsSummary.SpeedLimit < 1 ||
            _random.Next(60) == 0;

        if (applySpeedLimitChange)
        {
            _sensorsSummary.SpeedLimit = _random.Next(5, 15) * 5;
        }

        var applyMilesDrivenChange = _sensorsSummary.Odometer < 1 ||
            _sensorsSummary.NextFuelUp < 1 ||
            _random.Next(15) == 0;

        if (applyMilesDrivenChange)
        {
            var milesDriven = _random.Next(2);
            _sensorsSummary.Odometer += _sensorsSummary.Odometer < 1 ? 25000 : milesDriven;
            _sensorsSummary.NextFuelUp -= _sensorsSummary.NextFuelUp < 1 ? -150 : milesDriven;
        }

        SystemStatusCondition GetRandomSystemStatusCondition()
        {
            return _random.Next(60) == 0 ? SystemStatusCondition.Warning :
                _random.Next(60) == 0 ? SystemStatusCondition.Error :
                SystemStatusCondition.Good;
        }

        _sensorsSummary.TirePressureCondition = GetRandomSystemStatusCondition();
        _sensorsSummary.OilPressureCondition = GetRandomSystemStatusCondition();
        _sensorsSummary.TemperatureCondition = GetRandomSystemStatusCondition();
        _sensorsSummary.BatteryCondition = GetRandomSystemStatusCondition();
        _sensorsSummary.PowerSteeringCondition = GetRandomSystemStatusCondition();
        _sensorsSummary.EngineCondition = GetRandomSystemStatusCondition();
        _sensorsSummary.SensorsCondition = GetRandomSystemStatusCondition();
        _sensorsSummary.ProcessorCondition = GetRandomSystemStatusCondition();

        return applyRoadConditionChange || applySpeedLimitChange || applyMilesDrivenChange;
    }

    private bool RandomizeCurrentSpeed()
    {
        var applyUpdates = _sensorsSummary.CurrentSpeed < 1 || _random.Next(60) == 0;
        if (applyUpdates)
        {
            var speedChange = _random.Next(-1, 2);
            _sensorsSummary.CurrentSpeed += _sensorsSummary.CurrentSpeed < 1 ? 60 : speedChange;

            _sensorsSummary.SpeedChange = speedChange switch
            {
                1 => SpeedChange.Increasing,
                -1 => SpeedChange.Descreasing,
                _ => SpeedChange.None
            };

            _sensorsSummary.SignalDirection = _random.Next(60) == 0 ? SignalDirection.Left :
                _random.Next(60) == 0 ? SignalDirection.Right :
                SignalDirection.None;

            _sensorsSummary.Gear = _random.Next(60) == 0 ? Gear.Neutral :
                _random.Next(60) == 0 ? Gear.Reverse :
                _random.Next(60) == 0 ? Gear.Park :
                Gear.Drive;
        }
        else
        {
            if (_sensorsSummary.SpeedChange != SpeedChange.None)
            {
                _sensorsSummary.SpeedChange = SpeedChange.None;
                applyUpdates = true;
            }
            if (_sensorsSummary.SignalDirection != SignalDirection.None)
            {
                _sensorsSummary.SignalDirection = SignalDirection.None;
                applyUpdates = true;
            }
            if (_sensorsSummary.Gear != Gear.Drive)
            {
                _sensorsSummary.Gear = Gear.Drive;
                applyUpdates = true;
            }
        }
        return applyUpdates;
    }

    private bool RandomizeSystemMessages()
    {
        var applyUpdates = _random.Next(60) == 0;

        if (applyUpdates)
        {
            _sensorsSummary.SystemMessages.Clear();

            var messageCriticality = RandomArrayValue(Enum.GetValues<SystemMessageCriticality>().Skip(1).ToArray());

            var message = messageCriticality switch
            {
                SystemMessageCriticality.Information => "Next turn in 1 mile",
                SystemMessageCriticality.Warning => "Brakes Applied - Side collision avoided",
                _ => string.Empty
            };

            var systemMessage = new SystemMessage
            {
                Order = 1,
                Message = message,
                MessageCriticality = messageCriticality
            };

            _speechSynthesis.SpeakSystemMessages(systemMessage);

            _sensorsSummary.SystemMessages.Add(systemMessage);
        }

        return applyUpdates;
    }

    private bool SystemsOverviewInitialized;
    private bool RandomizeSystemsOverview()
    {
        var applyUpdates = !SystemsOverviewInitialized || _random.Next(60) == 0;

        if (applyUpdates)
        {
            SystemsOverviewInitialized = true;
            _sensorsSummary.EngineRpms = _random.Next(15, 60) * 100;
            _sensorsSummary.AirIntake = _random.Next(70, 100);
            _sensorsSummary.Coolant = _random.Next(180, 200);
            _sensorsSummary.BatteryVoltage = ((decimal)_random.Next(100, 200)) / 10;
            _sensorsSummary.FrontDriverSideTirePsi = _random.Next(40, 45);
            _sensorsSummary.FrontPassengerSideTirePsi = _random.Next(40, 45);
            _sensorsSummary.BackDriverSideTirePsi = _random.Next(40, 45);
            _sensorsSummary.BackPassengerSideTirePsi = _random.Next(40, 45);
            _sensorsSummary.EngineOilLife = _sensorsSummary.EngineOilLife == 0 || _random.Next(60) == 0 ? _random.Next(800) : _sensorsSummary.EngineOilLife;
            _sensorsSummary.WasherFluidLevel = "Full";
            _sensorsSummary.BrakeLife = _sensorsSummary.BrakeLife == 0 || _random.Next(60) == 0 ? _random.Next(1600) : _sensorsSummary.BrakeLife;
        }

        return applyUpdates;
    }

    private T RandomArrayValue<T>(IList<T> collection)
    {
        return collection[_random.Next(collection.Count)];
    }

    private bool RandomBool()
    {
        return _random.Next(0, 2) == 1;
    }
}