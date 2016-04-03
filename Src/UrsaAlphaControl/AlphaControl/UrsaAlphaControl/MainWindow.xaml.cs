using Pololu.UsbWrapper;
using Pololu.Usc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UrsaAlphaControl.VMS;

namespace UrsaAlphaControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UrsaVM ursa;
        Usc usc;
        Body body;
        Timer timer;
        public MainWindow()
        {
            InitializeComponent();
            body = new Body();
            ursa = new UrsaVM(body);
            this.DataContext = ursa;
            ursa.Connect.Executed += Connect_Executed;
            ursa.Disconnect.Executed += Disconnect_Executed;
            timer = new Timer(100);
            timer.AutoReset = true;
            timer.Start();
            timer.Elapsed += timer_Elapsed;
            
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            body.UpdateData();
            timer.Start();
        }

        void Disconnect_Executed(ICommand arg1, object arg2)
        {
            try {
                usc.disconnect();
            }
            catch{

            }
            body.Device = null;
            usc = null;
            ursa.IsConnected = false;
        }

        void Connect_Executed(ICommand arg1, object arg2)
        {
            try {
                usc = connectToDevice();
                body.Device = usc;
                ursa.IsConnected = true;
                body.StarupServoSetup();
            }
            catch {
                ursa.IsConnected = false;
            }
        }
        void TrySetTarget(Byte channel, UInt16 target)
        {
            if (usc == null)
                return;
            try
            {
               usc.setTarget(channel, target);
            }
            catch (Exception exception)  // Handle exceptions by displaying them to the user.
            {

            }
        }
        Usc connectToDevice()
        {
            // Get a list of all connected devices of this type.
            var connectedDevices = Usc.getConnectedDevices();

            foreach (DeviceListItem dli in connectedDevices)
            {
                // If you have multiple devices connected and want to select a particular
                // device by serial number, you could simply add a line like this:
                //   if (dli.serialNumber != "00012345"){ continue; }

                var device = new Usc(dli); // Connect to the device.
                return device;             // Return the device.
            }
            throw new Exception("Could not find device.  Make sure it is plugged in to USB " +
                "and check your Device Manager (Windows) or run lsusb (Linux).");
        }

    }
}
