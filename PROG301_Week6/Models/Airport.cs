﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PROG301_Week6.Models
{
    public class Airport
    {
        //private vars
        protected List<AerialVehicle> vehicles;
        protected int maxvehicles;
        protected string airportcode;


        //public vars
        public List<AerialVehicle> Vehicles { get { return vehicles; } set { vehicles = value; } }
        public int MaxVehicles { get { return maxvehicles; } set { maxvehicles = value; } }
        public string AirportCode { get { return airportcode; } set { airportcode = value; } }

        private static int defaultMaxVehicles = 5;

        //events

        public Airport(string Code) : this(Code, defaultMaxVehicles)
        {
            //Default to 5 vehicles   
            AirportCode = Code;
            MaxVehicles = defaultMaxVehicles;

            Vehicles = new List<AerialVehicle>(MaxVehicles);
        }

        public Airport(string Code, int Max)
        {
            AirportCode = Code;
            MaxVehicles = Max;

            Vehicles = new List<AerialVehicle>(MaxVehicles);
        }

        public Airport(string Code, int Max, List<AerialVehicle> vehicles)
        {
            AirportCode = Code;
            MaxVehicles = Max;

            Vehicles = vehicles;
        }

        public string Land(AerialVehicle a)
        {
            //Don't allow more vehicle to lan than the max
            if (Vehicles.Count < MaxVehicles)
            {
                //Set vehicle altitude to 0
                if (a.CurrentAltitude > 0)
                {
                    a.FlyDown(a.CurrentAltitude);
                }

                Vehicles.Add(a);

                return string.Format("{0} lands at {1}", a, AirportCode);
            }
            return string.Format("{0} is full can't land here", AirportCode);
        }

        public string TakeOff(AerialVehicle a)
        {

            string result = string.Empty;

            // Subscribe to the TakeOffEvent
            a.TakeOffEvent += message => result = message;

            // Perform takeoff operation

            Vehicles.Remove(a);

            a.StartEngine();
            a.TakeOff(); // This will trigger the TakeOffEvent

            return result + " from " + AirportCode;
        }

        public string AllTakeOff()
        {
            string allTakeOff = string.Empty;

            foreach (var vehicle in Vehicles.ToList())
            {
                vehicle.TakeOffEvent += result => allTakeOff += result + " from " + AirportCode;
                TakeOff(vehicle);
            }

            return allTakeOff;
        }

        public string Land(List<AerialVehicle> landing)
        {
            //Source: 
            //https://stackoverflow.com/questions/12231569/is-there-in-c-sharp-a-method-for-listt-like-resize-in-c-for-vectort
            //Resize List, remove extra values 
            if (landing.Count() > MaxVehicles) landing.RemoveRange(MaxVehicles, landing.Count() - MaxVehicles);

            string stringLand = string.Empty;
            foreach (AerialVehicle av in landing)
            {
                stringLand += Land(av);
            }

            return stringLand;
        }

    }
}
