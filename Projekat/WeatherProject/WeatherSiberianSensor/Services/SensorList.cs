using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorDeviceService.Services
{
    public class SensorsList
    {
        private static SensorsList sensors_list = null;
        private static readonly object objLock = new object();
        private List<Sensor> _sensors;

        private SensorsList()
        {
            _sensors = new List<Sensor>();
            _sensors.Add(new Sensor("temperature", 25, "./Resources/temperature.txt"));
            _sensors.Add(new Sensor("fire", 4.0, "./Resources/fire.txt"));
            _sensors.Add(new Sensor("precipitation", 0.5, "./Resources/precipitation.txt"));
            _sensors.Add(new Sensor("pressure", 1013, "./Resources/pressure.txt"));
            _sensors.Add(new Sensor("wind", 15, "./Resources/wind.txt"));
            _sensors.Add(new Sensor("wather-level", 15, "./Resources/wather-level.txt"));
            _sensors.Add(new Sensor("visibility", 15, "./Resources/visibility.txt"));
            _sensors.Add(new Sensor("humidity", 15, "./Resources/humidity.txt"));


        }

        public List<Sensor> GetSensors()
        {
            if (sensors_list == null)
                sensors_list = new SensorsList();
            return _sensors;
        }

        public static SensorsList GetSensorsListInstance()
        {
            if (sensors_list == null)
            {
                lock (objLock)
                {
                    if (sensors_list == null)
                    {
                        sensors_list = new SensorsList();
                    }
                }
            }

            return sensors_list;
        }
    }
}
