using System;
using System.Windows;
using System.Windows.Threading;

namespace Finalizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.IsEnabled = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblInfo.Content = string.Format("[{1}] Number of instances: {0}", Collector.NumberOfInstances.ToString("N0"), DateTime.Now.ToString("HH:mm:ss"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Collector c;
            for (int i = 1; i <= 500; i++)
            {
                c = new Collector();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GC.Collect();
        }
    }

    public class Collector
    {
        public static int NumberOfInstances;

        public Collector()
        {
            NumberOfInstances++;
        }

        ~Collector()
        {
            NumberOfInstances--;
        }
    }
}
