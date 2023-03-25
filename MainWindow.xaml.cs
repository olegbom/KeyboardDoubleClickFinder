using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeyboardDoubleClickFinder
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
        
        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            viewModel.Handler(e);
            e.Handled = true;

        }
        
        private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.Reset();
        }
    }
}
