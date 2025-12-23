namespace FitnessActivityTracker.Core.Models;

public class Workout
{
    public int Id { get; set; }
    public WorkoutType Type { get; set; } // Бег, Силовая, Йога, Плавание, Велосипед
    public DateTime Date {  get; set; } // обязательное поле
    public int DurationMinutes { get; set; } // в минутах
    public Intensity Intensity { get; set; } // Низкая, Средняя, Высокая
    public double BurnedCalories { get; set; } // расчетное значение
    public double? DistanceKm { get; set; } // в км, для кардио-тренировок
    public string? Notes { get; set; } // самочувствие, достижения)
    public int Rating { get; set; } // 1 - 5 stars
    public WorkoutStatus Status { get; set; } // Запланирована, Выполнена, Отменена
}