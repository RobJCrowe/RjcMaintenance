using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using maintLibrary;

namespace RjcMaintenanceConfig
{
    /// <summary>
    /// Interaction logic for AddEditService.xaml
    /// </summary>
    public partial class AddEditService : Window
    {
        Settings _settings;
        service _service;
        bool _isNew;
        int _index;
        public AddEditService(Settings settings, service service, bool isNew, int index)
        {
            InitializeComponent();
            _settings = settings;_service = service;_isNew = isNew; _index = index;
            if (isNew == true) { Title = "Add Service"; }
            else
            {
                Title = "Edit Service";
                tbName.Text = service.Name; tbLocation.Text = service.location; 
                tbArgs.Text = service.additionalArgs; cbActive.IsChecked = service.Active;
            }
        }

        private void Browse_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            if (openFileDialog.ShowDialog() == true)
                tbLocation.Text = openFileDialog.FileName;
        }

        private void Exit_Button(object sender, RoutedEventArgs e) { this.Close(); }
        
        private void Save_Button(object sender, RoutedEventArgs e)
        {
            _service.Name = tbName.Text; _service.location = tbLocation.Text; _service.additionalArgs = tbArgs.Text;
            _service.Active = cbActive.IsChecked ?? false;
            if (_isNew) { _settings.addService(_service); }
            else { _settings.editService(_service, _index); }
            this.Close();
        }
    }
}
