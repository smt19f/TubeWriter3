using System.Windows;
using TubeWriter3_WPF.ViewModels;

namespace TubeWriter3_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Submit();
        }

        public void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Clear();
        }

        public void AddButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }
    }
}
