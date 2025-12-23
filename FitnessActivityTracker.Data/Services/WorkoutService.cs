using System.Data.Entity;
using FitnessActivityTracker.Core.Models;
using FitnessActivityTracker.Core.Services;

namespace FitnessActivityTracker.Data.Services;

public class WorkoutService : IWorkoutService
{
    public async Task<IEnumerable<Workout>> GetAllWorkoutsAsync()
    {
        var context = await FitnessDbContext.GetContext();
        var list = await context.Workouts.ToListAsync();
        return list;
    }

    public async Task<IEnumerable<Workout>> GetWorkoutsByDateRange(DateTime start, DateTime end)
    {
        var context = await FitnessDbContext.GetContext();
        var list = context.Workouts.Where(x => x.Date > start && x.Date < end);
        return list;
    }

    public async Task<IEnumerable<Workout>> GetCompleteWorkoutsAsync()
    {
        var context = await FitnessDbContext.GetContext();
        var list = context.Workouts.Where(x => x.Status == WorkoutStatus.Completed);
        return list;
    }

    public async Task<Workout> AddWorkoutAsync(Workout workout)
    {
        var context = await FitnessDbContext.GetContext();
        var result = await context.AddAsync(workout);
        return result.Entity;
    }

    public async Task UpdateWorkoutAsync(Workout workout)
    {
        var context = await FitnessDbContext.GetContext();
        context.Update(workout);
    }

    public async Task<bool> DeleteWorkoutAsync(Workout workout)
    {
        var context = await FitnessDbContext.GetContext();
        try
        {
            context.Remove(workout);
            return true;
        }
        catch
        {
            return false; 
        }
    }

    public async Task<WorkoutStatistics> GetWorkoutStatisticsAsync(DateTime start, DateTime end)
    {
        var context = await FitnessDbContext.GetContext();
        throw new NotImplementedException();
    }
}