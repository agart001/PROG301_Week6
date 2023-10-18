using PROG301_Week6.ViewModels;
using System;
using System.Collections.Generic;
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
using PROG301_Week6.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace PROG301_Week6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AirportViewModel ap_vm;
        ObservableCollection<AerialVehicle> Arrivials { get; set; }
        ObservableCollection<AerialVehicle> Departures { get; set; }

        public MainWindow()
        {
            Arrivials = new ObservableCollection<AerialVehicle>
            {
                new Airplane(),
                new Airplane(),
                new Airplane(),
            };

            Departures = new ObservableCollection<AerialVehicle>();

            ap_vm = new AirportViewModel
                (
                    new Airport
                    (
                        "AF8B95",
                        10,
                        new List<AerialVehicle>
                        {
                            new Airplane(),
                            new Airplane(),
                            new Airplane(),
                        }
                    )
                );

            InitializeComponent();

            grd_AP.DataContext = ap_vm;

            dtgrd_arrivials.ItemsSource = Arrivials;
            dtgrd_departures.ItemsSource = Departures;
        }

        private void btn_SingleTakeOff_Click(object sender, RoutedEventArgs e)
        {

            ap_vm.SingleTakeOff(tb_SingleTakeOff.Text, Departures);

        }

        private void btn_MultiTakeOff_Click(object sender, RoutedEventArgs e)
        {

            ap_vm.MultiTakeOff(Departures);

        }

        private void btn_SingleLand_Click(object sender, RoutedEventArgs e)
        {
            ap_vm.SingleLand(tb_SingleLand.Text, Arrivials);
        }

        private void btn_MultiLand_Click(object sender, RoutedEventArgs e)
        {
            ap_vm.MultiLand(tb_MultiLandMin.Text, tb_MultiLandMax.Text, Arrivials);
        }
    }
}
