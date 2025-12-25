using System.Windows;
using FitnessActivityTracker.Core.Services;
using FitnessActivityTracker.Data.Services;
using FitnessActivityTrackerUI.ViewModels;
using FitnessActivityTrackerUI.Windows;

namespace FitnessActivityTrackerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var workoutService = new WorkoutService();
            var userSettingsService = new UserSettingsService();
            var calorieCalculatorService = new CalorieCalculatorService();

            DataContext = new MainWindowViewModel(workoutService, calorieCalculatorService, userSettingsService);
        }
    }
}