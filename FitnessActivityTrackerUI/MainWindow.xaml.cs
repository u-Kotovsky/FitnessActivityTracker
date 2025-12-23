using System.Windows;
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

            Hide();
            CreateGenericWindow();
            Close();
        }

        private GenericWindow CreateGenericWindow()
        {
            var genericWindow = new GenericWindow(new GenericViewModel());
            genericWindow.Show();
            return genericWindow;
        }
    }
}