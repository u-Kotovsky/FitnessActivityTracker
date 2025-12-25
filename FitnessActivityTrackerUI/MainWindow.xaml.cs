using System.Windows;
using FitnessActivityTracker.Core.Services;
using FitnessActivityTracker.Data;
using FitnessActivityTracker.Data.Services;
using FitnessActivityTrackerUI.ViewModels;

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

            var workoutService = new WorkoutService(FitnessDbContext.GetContext());
            var userSettingsService = new UserSettingsService();
            var calorieCalculatorService = new CalorieCalculatorService();

            var vm =  new MainWindowViewModel(workoutService, calorieCalculatorService, userSettingsService);

            DataContext = vm;
        }
    }
}