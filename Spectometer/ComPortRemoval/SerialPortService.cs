using System;

using System.IO.Ports;
using System.Linq;
using System.Management;

namespace Spectometer
{
    public static  class SerialPortService
    {
        private static string[] _serialPorts;

        private static ManagementEventWatcher arrival;

        private static ManagementEventWatcher removal;

        static SerialPortService()
        {
            _serialPorts = GetAvailableSerialPorts();
            MonitorDeviceChanges();
        }

      /// <summary>
      /// 
      /// </summary>
        public static void CleanUp()
        {
            arrival.Stop();
            removal.Stop();
        }
        /// <summary>
        /// 
        /// </summary>
        public static event EventHandler<PortsChangedArgs> PortsChanged;
       

        private static void MonitorDeviceChanges()
        {
            try
            {
                var deviceArrivalQuery = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
                var deviceRemovalQuery = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");

                arrival = new ManagementEventWatcher(deviceArrivalQuery);
                removal = new ManagementEventWatcher(deviceRemovalQuery);

                arrival.EventArrived += (o, args) => RaisePortsChangedIfNecessary(EventType.Insertion);
                removal.EventArrived += (sender, eventArgs) => RaisePortsChangedIfNecessary(EventType.Removal);

                // Start listening for events
                arrival.Start();
                removal.Start();
            }
            catch (ManagementException err)
            {

            }
        }

        private static void RaisePortsChangedIfNecessary(EventType eventType)
        {
            lock (_serialPorts)
            {
                var availableSerialPorts = GetAvailableSerialPorts();
                if (!_serialPorts.SequenceEqual(availableSerialPorts))
                {
                    _serialPorts = availableSerialPorts;

                    PortsChanged.Raise(null, new PortsChangedArgs(eventType, _serialPorts));
                }
            }
        }
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public static string[] GetAvailableSerialPorts()
        {
            return SerialPort.GetPortNames();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum EventType
    {
        Insertion,
        Removal,
    }

}