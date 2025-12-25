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

            RefreshCollections();

            for (int i = 0; i < 31; i++)
            {
                CalendarItems.Add(i + 1);
            }

            //ComingWorkouts.Add(new Workout());
        }

        private void RefreshCollections()
        {
            Workouts = [.. _workoutService.GetAllWorkouts()];
            CalendarItems = [];
            ComingWorkouts = [.. _workoutService.GetAllWorkouts().Where(x => x.Status == WorkoutStatus.Planned)];
        }

        private ObservableCollection<Workout> workouts;
        public ObservableCollection<Workout> Workouts { get { return workouts; } set { workouts = value; OnPropertyChanged(); } }
        private ObservableCollection<int> calendarItems;
        public ObservableCollection<int> CalendarItems { get { return calendarItems; } set { calendarItems = value; OnPropertyChanged(); } }
        public ObservableCollection<Workout> comingWorkouts;
        public ObservableCollection<Workout> ComingWorkouts { get { return comingWorkouts; } set { comingWorkouts = value; OnPropertyChanged(); } }


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
                    vm.Callback += () =>
                    { 
                        window.Hide();
                        RefreshCollections();
                        window.Close();

                    };
                    window.ShowDialog();
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
                    vm.Callback += () =>
                    {
                        RefreshCollections();
                    };
                    window.ShowDialog();
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
                    if (MessageBox.Show("Вы уверены, что хотите отменить тренировку?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
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