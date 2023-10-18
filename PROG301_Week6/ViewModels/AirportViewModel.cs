using PROG301_Week6.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static PROG301_Week6.Utility;

namespace PROG301_Week6.ViewModels
{
    public class AirportViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public AirportViewModel(Airport ap)
        {
            airport = ap;
        }


        public Airport airport;

        public ObservableCollection<AerialVehicle> Vehicles { get { return IEnumToObsCol(airport.Vehicles); } 
            set 
            { 
                airport.Vehicles = ObsColToList(value); 
                RaisePropertyChangedEvent(); 
            }}
        public int MaxVehicles { get { return airport.MaxVehicles; } set { airport.MaxVehicles = value; RaisePropertyChangedEvent(); } }
        public string AirportCode { get { return airport.AirportCode; } set { airport.AirportCode = value; RaisePropertyChangedEvent(); } }


        public void SingleTakeOff(string input, ObservableCollection<AerialVehicle> departures)
        {
            int value = Convert.ToInt16(input);

            AerialVehicle index = Vehicles[value];

            departures.Add(index);

            airport.TakeOff(index);

            RaisePropertyChangedEvent("Vehicles");
        }


        public void MultiTakeOff(ObservableCollection<AerialVehicle> departures)
        {
            foreach ( var av in Vehicles )
            {
                departures.Add(av);
            }

            airport.AllTakeOff();

            RaisePropertyChangedEvent("Vehicles");
        }

        public void SingleLand(string input, ObservableCollection<AerialVehicle> arrivials)
        {
            int value = Convert.ToInt16(input);

            AerialVehicle index = arrivials[value];

            if (Vehicles.Count + 1 > MaxVehicles)
            {
                return;
            }

            arrivials.Remove(index);

            airport.Land(index);

            RaisePropertyChangedEvent("Vehicles");
        }


        public void MultiLand(string smin, string smax, ObservableCollection<AerialVehicle> arrivials)
        {
            int min = Convert.ToInt16(smin);
            int max = Convert.ToInt16(smax);

            List<AerialVehicle> range = arrivials.Skip(min).Take(max).ToList();

            range.ForEach(av => arrivials.Remove(av));

            airport.Land(range);

            RaisePropertyChangedEvent("Vehicles");
        }
    }
}
