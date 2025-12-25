using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitnessActivityTracker.Core.Models;

public class Workout : INotifyPropertyChanged
{
    private int id;
    public int Id { get { return id; } set { id = value; OnPropertyChanged(); } }

    // Бег, Силовая, Йога, Плавание, Велосипед
    private WorkoutType type;
    public WorkoutType Type { get { return type; } set { type = value; OnPropertyChanged(); } }

    // обязательное поле
    private DateTime date;
    public DateTime Date { get { if (date == default) date = DateTime.Now; return date; } set { date = value; OnPropertyChanged(); } }

    // в минутах
    private int durationMinutes;
    public int DurationMinutes { get { return durationMinutes; } set { durationMinutes = value; OnPropertyChanged(); } }

    // Низкая, Средняя, Высокая
    private Intensity intensity;
    public Intensity Intensity { get { return intensity; } set { intensity = value; OnPropertyChanged(); } }

    // расчетное значени
    private double burnedCalories;
    public double BurnedCalories { get { return burnedCalories; } set { burnedCalories = value; OnPropertyChanged(); } }

    // в км, для кардио-тренировок
    private double? distanceKm;
    public double? DistanceKm { get { return distanceKm; } set { distanceKm = value; OnPropertyChanged(); } }

    // самочувствие, достижения)
    private string? notes;
    public string? Notes { get { return notes; } set { notes = value; OnPropertyChanged(); } }

    // 1 - 5 stars
    private int rating;
    public int Rating { get { return rating; } set { rating = value; OnPropertyChanged(); } }

    // Запланирована, Выполнена, Отменена
    private WorkoutStatus status;
    public WorkoutStatus Status { get { return status; } set { status = value; OnPropertyChanged(); } }


    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}