using FitnessActivityTracker.Core.Models;

namespace FitnessActivityTracker.Core.Services
{
    public interface IWorkoutService
    {
        Task<IEnumerable<Workout>> GetAllWorkoutsAsync();
        Task<IEnumerable<Workout>> GetWorkoutsByDateRange(DateTime start, DateTime end);
        Task<IEnumerable<Workout>> GetCompleteWorkoutsAsync();
        Task<Workout> AddWorkoutAsync(Workout workout);
        Task UpdateWorkoutAsync(Workout workout);
        Task<bool> DeleteWorkoutAsync(Workout workout);
        Task<WorkoutStatistics> GetWorkoutStatisticsAsync(DateTime start, DateTime end);
    }
}