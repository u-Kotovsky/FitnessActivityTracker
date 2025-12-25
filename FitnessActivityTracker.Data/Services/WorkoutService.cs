using System.Data.Entity;
using FitnessActivityTracker.Core.Models;
using FitnessActivityTracker.Core.Services;

namespace FitnessActivityTracker.Data.Services;

public class WorkoutService : IWorkoutService
{
    private IDbContext _dbContext;

    public WorkoutService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Workout> GetAllWorkouts()
    {
        var context = FitnessDbContext.GetContext();
        var list = context.Workouts.ToList();
        return list;
    }

    public async Task<IEnumerable<Workout>> GetAllWorkoutsAsync()
    {
        var context = await FitnessDbContext.GetContextAsync();
        var list = await context.Workouts.ToListAsync();
        return list;
    }

    public IEnumerable<Workout> GetWorkoutsByDateRange(DateTime start, DateTime end)
    {
        var context = FitnessDbContext.GetContext();
        var list = context.Workouts.Where(x => x.Date > start && x.Date < end);
        return list;
    }

    public async Task<IEnumerable<Workout>> GetWorkoutsByDateRangeAsync(DateTime start, DateTime end)
    {
        var context = await FitnessDbContext.GetContextAsync();
        var list = context.Workouts.Where(x => x.Date > start && x.Date < end);
        return list;
    }

    public IEnumerable<Workout> GetCompleteWorkouts()
    {
        var context = FitnessDbContext.GetContext();
        var list = context.Workouts.Where(x => x.Status == WorkoutStatus.Completed);
        return list;
    }

    public async Task<IEnumerable<Workout>> GetCompleteWorkoutsAsync()
    {
        var context = await FitnessDbContext.GetContextAsync();
        var list = context.Workouts.Where(x => x.Status == WorkoutStatus.Completed);
        return list;
    }

    public Workout AddWorkout(Workout workout)
    {
        var context = FitnessDbContext.GetContext();
        var result = context.Add(workout);
        return result.Entity;
    }

    public async Task<Workout> AddWorkoutAsync(Workout workout)
    {
        var context = await FitnessDbContext.GetContextAsync();
        var result = await context.AddAsync(workout);
        return result.Entity;
    }

    public void UpdateWorkout(Workout workout)
    {
        var context = FitnessDbContext.GetContext();
        context.Update(workout);
    }

    public async Task UpdateWorkoutAsync(Workout workout)
    {
        var context = await FitnessDbContext.GetContextAsync();
        context.Update(workout);
    }

    public bool DeleteWorkout(Workout workout)
    {
        var context = FitnessDbContext.GetContext();
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

    public async Task<bool> DeleteWorkoutAsync(Workout workout)
    {
        var context = await FitnessDbContext.GetContextAsync();
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

    public WorkoutStatistics GetWorkoutStatistics(DateTime start, DateTime end)
    {
        var context = FitnessDbContext.GetContext();
        throw new NotImplementedException();
    }

    public async Task<WorkoutStatistics> GetWorkoutStatisticsAsync(DateTime start, DateTime end)
    {
        var context = await FitnessDbContext.GetContextAsync();
        throw new NotImplementedException();
    }
}