using System;

namespace Traffic_Simulator_V3
{
    public class Vehicle 
    {
        public int Source { get; set; } // 0 == North, 1 == East, 2 == South, 3 == West
        public int Destination { get; set; }
        public int TurnSignal { get; set; }
        public int Position { get; set; }
        public int NumberCreated { get; set; }
        public string skin;
        

        static Random rnd = new Random();

        // 1 - define a delegate
        // 2 - define an event based on that delegate
        // 3 - raise the event

        public delegate void VehicleCreatedEventHandler(object source, VehicleCreatedEventArgs e);
        public event VehicleCreatedEventHandler VehicleCreated;

        public Vehicle()
        {

        }

        public void CreateVehicle(int source, int destination)
        {
            Source = source;
            Destination = destination;

            if (destination - source == 1 || destination - source == 3 || destination - source == 0) TurnSignal = -1;
            else if (destination - source == 2 || destination - source == -2) TurnSignal = 0;
            else if (destination - source == -1 || destination - source == 3) TurnSignal = 1;
                                    
            switch (rnd.Next(0, 4))
            {
                case 0:
                    skin = "FER";
                    break;
                case 1:
                    skin = "GTO";
                    break;
                case 2:
                    skin = "MB";
                    break;
                case 3:
                    skin = "MINI";
                    break;
                default:
                    break;
            }

            OnVehicleCreated();
        }

        protected virtual void OnVehicleCreated()
        { // notify subs
            if (VehicleCreated != null)
                VehicleCreated(this, new VehicleCreatedEventArgs(skin, Source, Destination, TurnSignal));
        }
    }

    public class VehicleCreatedEventArgs : EventArgs
    {
        public int source, // 0 == North, 1 == East, 2 == South, 3 == West 
                   destination,
                   turnSignal;
        public string skin;

        public VehicleCreatedEventArgs(string skin, int source, int destination, int turnSignal)
        {
            this.skin = skin;
            this.source = source;
            this.destination = destination;
            this.turnSignal = turnSignal;
        }
    }
}
