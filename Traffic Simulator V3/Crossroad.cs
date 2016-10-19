using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traffic_Simulator_V3
{
    /// <summary>
    /// Ši klasė:
    /// 3. Turi 1 delegatą;
    /// 4. Turi nestandartinį įvykį.
    /// </summary>
    
    class Crossroad
    {
        static Random rnd = new Random();

        public bool northSouth;
        public bool westEast;

        // 1 - define a delegate
        // 2 - define an event based on that delegate
        // 3 - raise the event

        public delegate void CrossroadCreatedEventHandler(object source, CrossroadCreatedEventArgs e);
        public event CrossroadCreatedEventHandler CrossroadCreated;


        public void CreateCrossroad()
        {
            if (rnd.Next(0, 2) == 1)
            {
                northSouth = true;
                westEast = false;
            }
            else
            {
                northSouth = false;
                westEast = true;
            }

            OnCrossroadCreated();
        }

        protected virtual void OnCrossroadCreated()
        { // notify subs
            if (CrossroadCreated != null)
                CrossroadCreated(this, new CrossroadCreatedEventArgs(northSouth, westEast));
        }

        public bool CheckIfGreen(Vehicle veh)
        {
            if ((veh.Source == 0 || veh.Source == 2) && northSouth)
            {
                return true;
            }
            else if ((veh.Source == 1 || veh.Source == 3) && westEast)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class CrossroadCreatedEventArgs : EventArgs
    {
        public bool northSouth;
        public bool westEast;

        public CrossroadCreatedEventArgs(bool northSouth, bool westEast)
        {
            this.northSouth = northSouth;
            this.westEast = westEast;
        }
    }
}
