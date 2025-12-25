using System.ComponentModel;
using System.Runtime.CompilerServices;
using FitnessActivityTracker.Core.Models;

namespace FitnessActivityTrackerUI.ViewModels
{
    public class WorkoutViewModel : INotifyPropertyChanged
    {
        private Workout _workout;

        public WorkoutViewModel(Workout workout)
        {
            _workout = workout;
        }

        public int Id 
        {
            get { return _workout.Id; } 
            set { _workout.Id = value; OnPropertyChanged(); }
        }

        public WorkoutType Type 
        { 
            get { return _workout.Type; } 
            set { _workout.Type = value; OnPropertyChanged(); }
        }

        public DateTime Date 
        { 
            get { return _workout.Date; }
            set { _workout.Date = value; OnPropertyChanged(); }
        }

        public int DurationMinutes 
        { 
            get { return _workout.DurationMinutes; }
            set { _workout.DurationMinutes = value; OnPropertyChanged(); } 
        }

        public Intensity Intensity 
        { 
            get { return _workout.Intensity; } 
            set { _workout.Intensity = value; OnPropertyChanged(); }
        }

        public double BurnedCalories 
        { 
            get { return _workout.BurnedCalories; } 
            set { _workout.BurnedCalories = value; OnPropertyChanged(); }
        }

        public double? DistanceKm 
        { 
            get { return _workout.DistanceKm; } 
            set { _workout.DistanceKm = value; OnPropertyChanged(); } 
        }

        public string? Notes 
        { 
            get { return _workout.Notes; } 
            set { _workout.Notes = value; OnPropertyChanged(); }
        }

        public int Rating 
        {
            get { return _workout.Rating; }
            set { _workout.Rating = value; OnPropertyChanged(); }
        }

        public WorkoutStatus Status
        {
            get { return _workout.Status; } 
            set { _workout.Status = value; OnPropertyChanged(); } 
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
