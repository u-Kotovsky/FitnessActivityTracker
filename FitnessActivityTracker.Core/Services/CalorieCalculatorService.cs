using FitnessActivityTracker.Core.Models;

namespace FitnessActivityTracker.Core.Services
{
    public class CalorieCalculatorService : ICalorieCalculatorService
    {
        private static readonly Dictionary<WorkoutType, Dictionary<Intensity, double>> CalorieCoefficients = new()
        {
            { WorkoutType.Run, new() { { Intensity.Low, .1 }, { Intensity.Medium, .15 }, { Intensity.High, .2 } } },
            { WorkoutType.Strength, new() { { Intensity.Low, .1 }, { Intensity.Medium, .15 }, { Intensity.High, .2 } } },
            { WorkoutType.Yoga, new() { { Intensity.Low, .03 }, { Intensity.Medium, .04 }, { Intensity.High, .05 } } },
            { WorkoutType.Swimming, new() { { Intensity.Low, .1 }, { Intensity.Medium, .15 }, { Intensity.High, .2 } } },
            { WorkoutType.Cycling, new() { { Intensity.Low, .1 }, { Intensity.Medium, .15 }, { Intensity.High, .2 } } }
        };

        public double CalculateCalories(WorkoutType type, int durationMinutes, Intensity intensity, double weightKg = 70)
        {
            if (!CalorieCoefficients.TryGetValue(type, out var intensityMap))
                return 0;

            if (!intensityMap.TryGetValue(intensity, out var coefficient))
                return 0;

            return coefficient * durationMinutes * weightKg;
        }
    }
}