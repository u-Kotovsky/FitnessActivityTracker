using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using FitnessActivityTracker.Core.Models;
using FitnessActivityTracker.Core.Services;

namespace FitnessActivityTrackerUI.ViewModels
{
    public class AddEditWorkoutViewModel
    {
        private readonly IWorkoutService _workoutService;
        private readonly ICalorieCalculatorService _calorieCalculator;
        private readonly IUserSettingsService _userSettingsService;

        public ObservableCollection<WorkoutType> WorkoutTypes { get; }
        public ObservableCollection<Intensity> Intensities { get; }
        public ObservableCollection<WorkoutStatus> Statuses { get; }

        #region Properties
        private Workout _workout;
        public Workout Workout
        {
            get { return _workout; }
            set { _workout = value; OnPropertyChanged(); }
        }

        private WorkoutViewModel _workoutViewModel;
        public WorkoutViewModel WorkoutViewModel 
        {
            get { return _workoutViewModel; }
            set { _workoutViewModel = value; } 
        }

        public WorkoutType SelectedType
        {
            get => Workout?.Type ?? default;
            set
            {
                if (Workout == null) return;
                Workout.Type = value;
                OnPropertyChanged();
            }
        }

        public DateTime SelectedDate
        {
            get => Workout?.Date ?? DateTime.Now;
            set
            {
                if (Workout == null) return;
                Workout.Date = value;
                OnPropertyChanged();
            }
        }

        public WorkoutStatus SelectedStatus
        {
            get => Workout?.Status ?? default;
            set
            {
                if (Workout == null) return;
                Workout.Status = value;
                OnPropertyChanged();
            }
        }

        public int? DurationMinutes
        {
            get => Workout?.DurationMinutes;
            set
            {
                if (Workout == null) return;
                Workout.DurationMinutes = value ?? default;
                OnPropertyChanged();
            }
        }

        public Intensity SelectedIntensity
        {
            get => Workout?.Intensity ?? default;
            set
            {
                if (Workout == null) return;
                Workout.Intensity = value;
                OnPropertyChanged();
            }
        }

        public double? DistanceKm
        {
            get => Workout?.DistanceKm;
            set
            {
                if (Workout == null) return;
                Workout.DistanceKm = value;
                OnPropertyChanged();
            }
        }

        public string Notes
        {
            get => Workout?.Notes ?? string.Empty;
            set
            {
                if (Workout == null) return;
                Workout.Notes = value;
                OnPropertyChanged();
            }
        }

        public int Rating
        {
            get => Workout?.Rating ?? 0;
            set
            {
                if (Workout == null || value < 1 || value > 5) return;
                Workout.Rating = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public AddEditWorkoutViewModel(
            IWorkoutService workoutService, 
            ICalorieCalculatorService calorieCalculator, 
            IUserSettingsService userSettings, 
            Workout workout)
        {
            _workoutService = workoutService;
            _calorieCalculator = calorieCalculator;
            _userSettingsService = userSettings;

            WorkoutTypes = [.. Enum.GetValues<WorkoutType>()];
            SelectedType = 0;
            Intensities = [.. Enum.GetValues<Intensity>()];
            SelectedIntensity = 0;
            Statuses = [.. Enum.GetValues<WorkoutStatus>()];
            SelectedStatus = 0;

            _workout = workout;
        }

        #region Commands
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??= new RelayCommand(obj =>
                {
                    Save();
                });
            }
            set
            {
                saveCommand = value;
            }
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ??= new RelayCommand(obj =>
                {
                    Cancel();
                });
            }
            set
            {
                cancelCommand = value;
            }
        }

        public event Action Callback = delegate { };
        #endregion

        private void Save()
        {
            if (!string.IsNullOrEmpty(InputDataError))
            {
                MessageBox.Show(InputDataError, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (Workout == null) throw new Exception("Workout is null");

                Workout.BurnedCalories = _calorieCalculator.CalculateCalories(
                    Workout.Type, Workout.DurationMinutes, Workout.Intensity, _userSettingsService.GetUserWeightKg());

                if (Workout.Id == 0)
                {
                    _workoutService.AddWorkout(Workout);
                }
                else
                {
                    _workoutService.UpdateWorkout(Workout);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }

            Callback?.Invoke();
        }

        private void Cancel()
        {
            Callback?.Invoke();
        }

        public string InputDataError = null;

        private void CalculateInputErrors()
        {
            StringBuilder errors = new StringBuilder();

            if (SelectedDate.Ticks < DateTime.Now.Ticks && SelectedStatus == WorkoutStatus.Planned)
                errors.AppendLine("Дата для запланированной тренировки не может быть в прошлом.");

            if (DurationMinutes <= 0)
                errors.AppendLine("Продолжительность должна быть больше 0.");

            if ((SelectedType == WorkoutType.Strength || SelectedType == WorkoutType.Yoga) && DistanceKm.HasValue && DistanceKm.Value > 0)
                errors.AppendLine("Дистанция не применима к силовым тренировкам.");
            if (DistanceKm.HasValue && DistanceKm.Value < 0)
                errors.AppendLine("Дистанция не может быть отрицательной.");

            if (Rating < 1 || Rating > 5)
                errors.AppendLine("Рейтинг должен быть от 1 до 5.");

            InputDataError = errors.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            CalculateInputErrors();
        }
    }
}