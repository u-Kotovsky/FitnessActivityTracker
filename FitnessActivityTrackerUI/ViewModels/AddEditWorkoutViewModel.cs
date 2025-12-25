using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
            Intensities = [.. Enum.GetValues<Intensity>()];
            Statuses = [.. Enum.GetValues<WorkoutStatus>()];

            _workout = workout;
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??= new RelayCommand(obj =>
                {
                    Save();
                    Callback?.Invoke();
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
                    Callback?.Invoke();
                });
            }
            set
            {
                cancelCommand = value;
            }
        }

        public event Action Callback = delegate { };


        private void Save()
        {
            if (!string.IsNullOrEmpty(this.InputDataError))
            {
                return;
            }

            try
            {
                if (Workout == null)
                    throw new Exception("Workout is null");

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

            // TODO: Call an event something like OnSave
        }

        private void Cancel()
        {
            // TODO: Cancel, call an event something like OnCancel
        }

        public string InputDataError => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(SelectedDate):
                        if (SelectedDate < DateTime.Today && SelectedStatus == WorkoutStatus.Planned)
                            return "Дата для запланированной тренировки не может быть в прошлом.";
                        break;
                    case nameof(DurationMinutes):
                        if (DurationMinutes <= 0)
                            return "Продолжительность должна быть больше 0.";
                        break;
                    case nameof(DistanceKm):
                        if ((SelectedType == WorkoutType.Strength || SelectedType == WorkoutType.Yoga) && DistanceKm.HasValue && DistanceKm.Value > 0)
                            return "Дистанция не применима к силовым тренировкам.";
                        if (DistanceKm.HasValue && DistanceKm.Value < 0)
                            return "Дистанция не может быть отрицательной.";
                        break;
                    case nameof(Rating):
                        if (Rating < 1 || Rating > 5)
                            return "Рейтинг должен быть от 1 до 5.";
                        break;
                }

                throw new Exception($"Unknown column name '{columnName}'");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}