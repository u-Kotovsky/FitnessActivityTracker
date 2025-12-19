namespace FitnessActivityTrackerLibrary;

public class WorkoutData
{
    public ulong Id { get; set; }
    public WorkoutType Type { get; set; } // Бег, Силовая, Йога, Плавание, Велосипед
    public ulong DateTime {  get; set; } // обязательное поле
    public double Duration { get; set; } // в минутах
    public WorkoutIntensity Intensity { get; set; } // Низкая, Средняя, Высокая
    public double BurnedCalories { get; set; } // расчетное значение
    public double Distance { get; set; } // в км, для кардио-тренировок
    public string Notes { get; set; } // самочувствие, достижения)
    public byte DifficultyRating { get; set; } // 1 - 5 stars
    public WorkoutStatus Status { get; set; } // Запланирована, Выполнена, Отменена
}
