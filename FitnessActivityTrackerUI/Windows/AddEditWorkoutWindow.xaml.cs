using System.Windows;
using FitnessActivityTrackerUI.ViewModels;

namespace FitnessActivityTrackerUI.Windows
{
    /// <summary>
    /// Interaction logic for AddEditWorkoutWindow.xaml
    /// </summary>
    public partial class AddEditWorkoutWindow : Window
    {
        private AddEditWorkoutViewModel viewModel;
        public AddEditWorkoutWindow(AddEditWorkoutViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            DataContext = this.viewModel;
        }
    }
}
