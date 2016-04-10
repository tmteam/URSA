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
using Ursa.Body;
using Ursa.Cerebellum;
using Ursa.Cerebellum.Maestro24Controller;
using Ursa.Cerebellum.Telemetry;
using UrsaAlphaControl.VMS;

namespace UrsaAlphaControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BodyVM ursa;
        Maestro24Controller cerebellum;
        Body body;
        Timer timer;
        ProtobufTelemetryWriter writer;
        public MainWindow()
        {
            InitializeComponent();
            cerebellum = new Maestro24Controller();
            body  = new Body(cerebellum);
            body.Configurate(GetDefaultSettings());
            ursa  = new BodyVM(body);

            this.DataContext = ursa;
            ursa.Connect.Executed += Connect_Executed;
            ursa.Disconnect.Executed += Disconnect_Executed;

            writer = new ProtobufTelemetryWriter();
            cerebellum.Transcription = writer;
            
            timer = new Timer(100);
            timer.AutoReset = true;
            timer.Start();
            timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            cerebellum.UpdateValues();
            timer.Start();
        }

        void Disconnect_Executed(ICommand arg1, object arg2)
        {
            try {
                cerebellum.Disconnect();
                ursa.IsConnected = false;
                writer.Stop();
              //  var telemetry = Ursa.Cerebellum.Tools.ReadTelemeteryFile(writer.DataFilePath);
            }
            catch{
                
            }
        }

        void Connect_Executed(ICommand arg1, object arg2)
        {
            try {
                cerebellum.Connect();
                cerebellum.StarupSetup();
                ursa.IsConnected = true;
                writer.Start();
            }
            catch(Exception ex) {
                try { cerebellum.Disconnect(); }
                catch { }
                ursa.IsConnected = false;
            }
        }

        BodySettings GetDefaultSettings()
        {
            return new BodySettings
            {
                Legs = new[]{
                      new LegSettings{
                           Type = LegType.FrontLeft,
                           ScapulaSettings  = new ServoSettings{
                                 Num = 0,
                                 Min = 1680,
                                 Max = 10200,
                                 DegreesAtMin = 0,
                                 DegreesAtMax = 180,
                           },
                            ThighSettings   = new ServoSettings{
                                 Num = 1,
                                 Min = 1680,
                                 Max = 10200,
                                 DegreesAtMin = 0,
                                 DegreesAtMax = 180,
                           },
                           ShinSettings     = new ServoSettings{
                                 Num = 2,
                                 Min = 1680,
                                 Max = 10200,
                                 DegreesAtMin = 0,
                                 DegreesAtMax = 180,
                           },
                           PressureSettings = new SensorSettings { Num = 3 },
                      },
                      new LegSettings{
                           Type = LegType.FrontRight,
                           ScapulaSettings  = new ServoSettings{
                                 Num = 6,
                                 Min = 1680,
                                 Max = 10200,
                                 DegreesAtMin = 0,
                                 DegreesAtMax = 180,
                           },
                            ThighSettings   = new ServoSettings{
                                 Num = 7,
                                 Min = 1680,
                                 Max = 10200,
                                 DegreesAtMin = 0,
                                 DegreesAtMax = 180,
                           },
                           ShinSettings     = new ServoSettings{
                                 Num = 8,
                                 Min = 1680,
                                 Max = 10200,
                                 DegreesAtMin = 0,
                                 DegreesAtMax = 180,
                           },
                           PressureSettings = new SensorSettings { Num = 9 },
                      },
                      new LegSettings{
                           Type = LegType.BackLeft,
                           ScapulaSettings  = new ServoSettings{
                                 Num = 12,
                                 Min = 1680,
                                 Max = 10200,
                                 DegreesAtMin = 0,
                                 DegreesAtMax = 180,
                           },
                            ThighSettings   = new ServoSettings{
                                 Num = 13,
                                 Min = 1680,
                                 Max = 10200,
                                 DegreesAtMin = 0,
                                 DegreesAtMax = 180,
                           },
                           ShinSettings     = new ServoSettings{
                                 Num = 14,
                                 Min = 1680,
                                 Max = 10200,
                                 DegreesAtMin = 0,
                                 DegreesAtMax = 180,
                           },
                           PressureSettings = new SensorSettings { Num = 15 },
                      },
                      new LegSettings{
                           Type = LegType.BackRight,
                           ScapulaSettings  = new ServoSettings{
                                 Num = 18,
                                 Min = 1680,
                                 Max = 10200,
                                 DegreesAtMin = 0,
                                 DegreesAtMax = 180,
                           },
                            ThighSettings   = new ServoSettings{
                                 Num = 19,
                                 Min = 1680,
                                 Max = 10200,
                                 DegreesAtMin = 0,
                                 DegreesAtMax = 180,
                           },
                           ShinSettings     = new ServoSettings{
                                 Num = 20,
                                 Min = 1680,
                                 Max = 10200,
                                 DegreesAtMin = 0,
                                 DegreesAtMax = 180,
                           },
                           PressureSettings = new SensorSettings { Num = 21 },
                      },
                 }
            };
        }
    }
}
