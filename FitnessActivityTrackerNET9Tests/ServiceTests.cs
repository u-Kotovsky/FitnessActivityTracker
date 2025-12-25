using FitnessActivityTracker.Core.Models;
using FitnessActivityTracker.Core.Services;
using Moq;
using NUnit.Framework;
using Xunit;

namespace FitnessActivityTracker.Tests
{
    public class ServiceTests
    {
        private IWorkoutService workoutService;


        [SetUp]
        public void Setup()
        {
            workoutService = Mock.Of<IWorkoutService>();
        }

        /*[Fact]
        public void GetWorkoutsByDateRange_WithValidRange_ReturnsFilteredWorkouts()
        {
            // TODO: isolate main db from test
            var start = new DateTime();
            var end = new DateTime();

            // Create test data

            // Fill it in service

            // Test target method

            var workoutsByDateRange = workoutService.GetWorkoutsByDateRange(start, end);

            // TODO: verify if all good

            Assert.Ignore();
        }

        [Fact]
        public void GetCompletedWorkotus_ShouldReturnCompletedWorkouts()
        {
            // TODO: isolate main db from test

            // Fill it in service

            // Test target method

            var workoutsByDateRange = workoutService.GetCompleteWorkoutsAsync()
                .GetAwaiter().GetResult();

            // TODO: verify if all good

            Assert.Ignore();
        }

        [Fact]
        public void AddWorkout_WithValidData_ShouldAddWorkoutToDatabase()
        {
            // TODO: isolate main db from test
            var workout = new Workout();

            // Create test data

            // Fill it in service

            // Test target method

            var workoutsByDateRange = workoutService.AddWorkoutAsync(workout)
                .GetAwaiter().GetResult();

            // TODO: verify if all good

            Assert.Ignore();
        }

        [Fact]
        public void UpdateWorkoutStatus_ShouldUpdateWorkoutStatus()
        {
            // TODO: isolate main db from test
            var workout = new Workout();

            // Create test data

            // Fill it in service

            // Test target method

            workoutService.UpdateWorkoutAsync(workout)
                .GetAwaiter().GetResult();

            // TODO: verify if all good

            Assert.Ignore();
        }

        [Fact]
        public void GetWorkoutStatistics_ReturnsWorkoutStatistics()
        {
            // TODO: isolate main db from test
            var start = new DateTime();
            var end = new DateTime();
            var workout = new Workout();

            var workoutStatistics = workoutService.GetWorkoutStatisticsAsync(start, end)
                .GetAwaiter().GetResult();

            // TODO: verify if all good

            Assert.Ignore();
        }*/

        [Fact]
        public void CalculateCalories_WithValidData_ShouldReturnValidResult()
        {
            var calorieCalculator = new CalorieCalculatorService();

            var actual = .1f * 10 * 70;
            var calories = calorieCalculator.CalculateCalories(WorkoutType.Run, 10, Intensity.Medium, 70);

            Assert.Equals(calories, actual);
            Assert.That(true, Is.True);
        }
    }
}
