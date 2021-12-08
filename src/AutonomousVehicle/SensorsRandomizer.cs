using AutonomousVehicle.Models;

namespace AutonomousVehicle;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA5394:Do not use insecure randomness", Justification = "This is not security critical")]
public class SensorsRandomizer
{
    private readonly SensorsSummary _sensorsSummary;
    private readonly Action _onRandomize;
    private readonly Random _random;

    public SensorsRandomizer(SensorsSummary sensorsSummary, Action onRandomize)
    {
        _sensorsSummary = sensorsSummary;
        _onRandomize = onRandomize;
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
                await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
                return;
            }
        }
    }

    private void Randomize()
    {
        var applyUpdates = false;
        applyUpdates |= RandomizeDestination();
        applyUpdates |= RandomizeTemperature();

        if (applyUpdates)
            _onRandomize();
    }

    private bool RandomizeDestination()
    {
        var applyUpdates = string.IsNullOrWhiteSpace(_sensorsSummary.Destination) || !_sensorsSummary.TravelTimeMinutes.HasValue || _random.Next(60) == 0;
        if (applyUpdates)
        {
            _sensorsSummary.Destination = RandomString(15);
            _sensorsSummary.TravelTimeMinutes = _random.Next(60);
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

    private T RandomArrayValue<T>(IList<T> collection)
    {
        return collection[_random.Next(collection.Count)];
    }

    private string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => RandomArrayValue(s.ToCharArray())).ToArray());
    }
}