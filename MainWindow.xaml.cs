using NetworkMonitor.Models;
using NetworkMonitor.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace NetworkMonitor
{
    public partial class MainWindow : Window
    {
        private readonly VisualizationService visualizationService;
        public MainWindow()
        {
            InitializeComponent();
            visualizationService = new VisualizationService();
            GetData();
        }

        private void GetData()
        {
            PacketsGrid.Columns.Clear();
            PacketsGrid.ItemsSource = visualizationService.GetData();
        }

        private async void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var row = (GridItem)((DataGridRow)sender).DataContext;
            var service = new GeoService();
            try
            {
                var geoposition = await service.GetGeopositionAsync(row.RemoteAddress);
                var message = $"Country: {geoposition.Country}\nRegion: {geoposition.RegionName}\nCity: {geoposition.City}\nOrganization: {geoposition.Organization}";
                MessageBox.Show(message, "Geoposition", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetData();
        }
    }
}
