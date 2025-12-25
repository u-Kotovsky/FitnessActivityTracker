using FitnessActivityTracker.Core.Models;

namespace FitnessActivityTracker.Core.Services
{
    public interface IWorkoutService
    {
        IEnumerable<Workout> GetAllWorkouts();
        Task<IEnumerable<Workout>> GetAllWorkoutsAsync();

        IEnumerable<Workout> GetWorkoutsByDateRange(DateTime start, DateTime end);
        Task<IEnumerable<Workout>> GetWorkoutsByDateRangeAsync(DateTime start, DateTime end);

        IEnumerable<Workout> GetCompleteWorkouts();
        Task<IEnumerable<Workout>> GetCompleteWorkoutsAsync();

        Workout AddWorkout(Workout workout);
        Task<Workout> AddWorkoutAsync(Workout workout);

        void UpdateWorkout(Workout workout);
        Task UpdateWorkoutAsync(Workout workout);

        bool DeleteWorkout(Workout workout);
        Task<bool> DeleteWorkoutAsync(Workout workout);

        WorkoutStatistics GetWorkoutStatistics(DateTime start, DateTime end);
        Task<WorkoutStatistics> GetWorkoutStatisticsAsync(DateTime start, DateTime end);
    }
}