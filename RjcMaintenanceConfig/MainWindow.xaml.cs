using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using maintLibrary;

namespace RjcMaintenanceConfig
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Settings settings;
        //ObservableCollection<service> tempServices = new ObservableCollection<service>();
        public MainWindow() { InitializeComponent(); }
        private void resetSelectionIndex() { testDgv.SelectedIndex = -1; }
        private bool hasSelection() 
        {
            if (testDgv.SelectedIndex == -1)
            {
                MessageBox.Show("You must make a selection.", "Warning!");
                return false;
            }
            return true;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            resetSelectionIndex();
            service temp = new service();
            AddEditService aeService = new AddEditService(settings, temp, true, 0);
            aeService.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            settings = Settings.GetSettings();
            testDgv.ItemsSource = settings.services;
            testDgv.CanUserAddRows = false;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) { Environment.Exit(0); }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ObservableCollection<service> temp = new ObservableCollection<service>();
            temp = settings.services;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (hasSelection())
            {
                AddEditService aeService = new AddEditService(settings, settings.services[testDgv.SelectedIndex], false, testDgv.SelectedIndex);
                resetSelectionIndex();
                aeService.ShowDialog();
            }
        }
    }
}
