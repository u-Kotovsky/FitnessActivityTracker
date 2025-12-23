using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FitnessActivityTracker.Core.Models;
using FitnessActivityTracker.Core.Services;

namespace FitnessActivityTrackerUI.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IWorkoutService _workoutService;
        private readonly ICalorieCalculatorService _calorieCalculator;
        private readonly IUserSettingsService _userSettingsService;

        public MainWindowViewModel(
            IWorkoutService workoutService,
            ICalorieCalculatorService calorieCalculator,
            IUserSettingsService userSettingsService)
        {
            _workoutService = workoutService;
            _calorieCalculator = calorieCalculator;
            _userSettingsService = userSettingsService;

            Workouts = [];
        }

        public ObservableCollection<Workout> Workouts { get; }
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}