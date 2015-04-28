using System.Windows;
using QiMata.CPPTools.ClassGenerator.ViewModels;

namespace QiMata.CPPTools.ClassGenerator.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }
    }
}
