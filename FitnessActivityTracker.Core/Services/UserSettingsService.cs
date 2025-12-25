namespace FitnessActivityTracker.Core.Services
{
    public class UserSettingsService : IUserSettingsService
    {
        private double weightKg;
        public double WeightKg
        {
            get { return weightKg; }
            set { weightKg = value; }
        }

        public double GetUserWeightKg()
        {
            return WeightKg;
        }
    }
}
