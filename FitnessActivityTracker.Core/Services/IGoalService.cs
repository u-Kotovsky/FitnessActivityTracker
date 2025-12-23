namespace FitnessActivityTracker.Core.Services
{
    public interface IGoalService
    {
        Task<bool> IsWeeklyGoalAchievedAsync(int userId, DateTime weekStart);
        Task<bool> IsMonthlyGoalAchievedAsync(int userId, DateTime monthStart);
    }
}