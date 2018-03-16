using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace SciFi_HUD_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer Timer = new DispatcherTimer();

        public PerformanceCounter CPU = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        public PerformanceCounter RAM = new PerformanceCounter("Memory", "Available MBytes", null);
        
        
        public Int32 cpuCounter, ramCounter, totalRam = 0;

        public MainWindow()
        {
            InitializeComponent();

            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 1024);
            Timer.IsEnabled = true; 
        }

        static ulong GetTotalRAM()
        {
           return new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory/1024/1024;
        }

        public void Timer_Tick(System.Object sender, System.EventArgs e)

    {
            cpuCounter = Convert.ToInt32(CPU.NextValue());
            CPU_Usage.Content = cpuCounter.ToString() + "%";

            ramCounter = Convert.ToInt32(GetTotalRAM()) - Convert.ToInt32(RAM.NextValue());
            RAM_Usage.Content = ramCounter.ToString() + "Mb";
        }

        private void drag_view(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseHud(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void OpenBrowser(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://google.com");
        }

        private void OpenTerminal(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo("cmd.exe");
            Process.Start(info);
        }

    }
}
