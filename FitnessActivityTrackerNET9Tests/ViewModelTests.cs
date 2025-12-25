using System;
using System.Collections.Generic;
using System.Text;
using FitnessActivityTracker.Core.Models;
using FitnessActivityTrackerUI.ViewModels;
using NUnit.Framework;

namespace FitnessActivityTracker.Tests
{
    public class ViewModelTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test()
        {
            Assert.Ignore();
        }

        public void WorkoutViewModel_WithValidWorkout_ShouldReturnValidValues()
        {
            var workout = new Workout()
            {
                Id = 32,
                Type = WorkoutType.Run,
                Date = DateTime.Now,
                DurationMinutes = 30,
                Intensity = Intensity.Medium,
                BurnedCalories = 30,
                DistanceKm = 10,
                Notes = "100",
                Rating = 3,
                Status = WorkoutStatus.Planned
            };

            //var vm = new WorkoutViewModel(workout);

            Assert.Ignore();
        }
    }
}
