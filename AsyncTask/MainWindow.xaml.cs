using System.Threading.Tasks;
using System.Windows;

namespace AsyncTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Parallel Method
        private Task<int> ComputeAsync()
        {
            return Task.Run(() =>
            {
                System.Threading.Thread.Sleep(2500);
                return 42;
            });
        }
        #endregion
    }
}
