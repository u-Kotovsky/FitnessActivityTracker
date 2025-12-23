using FitnessActivityTracker.Core.Models;

namespace FitnessActivityTracker.Core.Services
{
    public interface ICalorieCalculatorService
    {
        double CalculateCalories(WorkoutType workoutType, int durationMinutes, Intensity intensity, double weightKg);
    }
}