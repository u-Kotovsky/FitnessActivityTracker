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
        }

        private void RefreshCollections()
        {
            Workouts = [.. _workoutService.GetAllWorkouts()];
            CalendarItems = [];
            for (int i = 0; i < 31; i++)
            {
                CalendarItems.Add(i + 1);
            }
            ComingWorkouts = [.. _workoutService.GetAllWorkouts()];
        }

        private ObservableCollection<Workout> workouts;
        public ObservableCollection<Workout> Workouts { get { return workouts; } set { workouts = value; OnPropertyChanged(); } }
        private ObservableCollection<int> calendarItems;
        public ObservableCollection<int> CalendarItems { get { return calendarItems; } set { calendarItems = value; OnPropertyChanged(); } }
        public ObservableCollection<Workout> comingWorkouts;
        public ObservableCollection<Workout> ComingWorkouts { get { return comingWorkouts; } set { comingWorkouts = value; OnPropertyChanged(); } }

        private Workout selectedWorkout;
        public Workout SelectedWorkout
        {
            get => selectedWorkout;
            set
            {
                selectedWorkout = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??= new RelayCommand(obj =>
                {
                    var vm = new AddEditWorkoutViewModel(_workoutService, _calorieCalculator, _userSettings, new Workout());
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
                    if (selectedWorkout == null) return;

                    var workout = selectedWorkout;
                    workout.Id = 0;
                    _workoutService.AddWorkout(workout);
                    RefreshCollections();
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
                    if (selectedWorkout == null) return;
                    var workout = selectedWorkout;
                    var vm = new AddEditWorkoutViewModel(_workoutService, _calorieCalculator, _userSettings, workout);
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

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ??= new RelayCommand(obj =>
                {
                    if (selectedWorkout == null) return;

                    if (MessageBox.Show("Вы уверены, что хотите отменить тренировку?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        selectedWorkout.Status = WorkoutStatus.Cancelled;
                        _workoutService.UpdateWorkout(selectedWorkout);
                        RefreshCollections();
                    }
                });
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??= new RelayCommand(obj =>
                {
                    if (selectedWorkout == null) return;

                    if (MessageBox.Show("Вы уверены, что хотите удалить тренировку?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        _workoutService.DeleteWorkout(selectedWorkout);
                        RefreshCollections();
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