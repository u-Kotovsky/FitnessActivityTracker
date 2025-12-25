using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FitnessActivityTracker.Core.Models;
using FitnessActivityTracker.Core.Services;
using FitnessActivityTrackerUI.Windows;

namespace FitnessActivityTrackerUI.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IWorkoutService _workoutService;
        private readonly ICalorieCalculatorService _calorieCalculator;
        private readonly IUserSettingsService _userSettings;

        public MainWindowViewModel(
            IWorkoutService workoutService,
            ICalorieCalculatorService calorieCalculator,
            IUserSettingsService userSettingsService)
        {
            _workoutService = workoutService;
            _calorieCalculator = calorieCalculator;
            _userSettings = userSettingsService;

            Workouts = [];
            CalendarItems = [];
            ComingWorkouts = [];

            for (int i = 0; i < 31; i++)
            {
                CalendarItems.Add(i + 1);
            }

            ComingWorkouts.Add(new Workout());
        }

        public ObservableCollection<Workout> Workouts { get; }
        
        public ObservableCollection<int> CalendarItems { get; }

        public ObservableCollection<Workout> ComingWorkouts { get; }


        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??= new RelayCommand(obj =>
                {
                    var vm = new AddEditWorkoutViewModel(
                        _workoutService, _calorieCalculator, _userSettings, new Workout());
                    var window = new AddEditWorkoutWindow(vm);
                });
            }
        }

        private RelayCommand duplicateCommand;
        public RelayCommand DuplicateCommand
        {
            get
            {
                return duplicateCommand ??= new RelayCommand(obj =>
                {
                    // TODO: duplicate
                });
            }
        }

        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??= new RelayCommand(obj =>
                {
                    var vm = new AddEditWorkoutViewModel(
                        _workoutService, _calorieCalculator, _userSettings, null
                        /* TODO: somehow take workout from onclick event?? */);
                    var window = new AddEditWorkoutWindow(vm);
                });
            }
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ??= new RelayCommand(obj =>
                {
                    var vm = new AddEditWorkoutViewModel(
                        _workoutService, _calorieCalculator, _userSettings, null
                        /* TODO: somehow take workout from onclick event?? */);
                    // confirm do you want to cancel workout or not
                    if (MessageBox.Show("", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        // TODO: cancel selected workout
                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}