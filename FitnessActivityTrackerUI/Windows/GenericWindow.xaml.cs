using System.Windows;
using FitnessActivityTrackerUI.ViewModels;

namespace FitnessActivityTrackerUI.Windows
{
    /// <summary>
    /// Interaction logic for GenericWindow.xaml
    /// </summary>
    public partial class GenericWindow : Window
    {
        private GenericViewModel viewModel;

        public GenericWindow(GenericViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            DataContext = this.viewModel;
        }
    }
}