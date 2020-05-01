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
        private void loadValues() { Log.IsChecked = settings.logToDb; TestDB.IsChecked = settings.testDB;
            password.Password = settings.password; }
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
            loadValues();
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
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if(!hasSelection()) return;
            if (settings.removeService(testDgv.SelectedIndex)) { resetSelectionIndex(); }
        }
        private void Up_Click(object sender, RoutedEventArgs e)
        {
            if (hasSelection()) { testDgv.SelectedIndex = settings.serviceUp(testDgv.SelectedIndex); }
        }
        private void Down_Click(object sender, RoutedEventArgs e)
        {
            if (hasSelection()) { testDgv.SelectedIndex = settings.serviceDown(testDgv.SelectedIndex); }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            settings.password = (Log.IsChecked==true)?password.Password:"";
            if (settings.WriteSettings()) { write("Settings saved."); }
            else { write("Writing settings failed."); }
        }
        private void write(string s) { MessageBox.Show(s); }
        private void Log_Click(object sender, RoutedEventArgs e) { settings.logToDb = Log.IsChecked ?? false; }

        private void TestDB_CheckBox_Click(object sender, RoutedEventArgs e) 
        {
            settings.testDB = TestDB.IsChecked ?? false;
        }
    }
}
