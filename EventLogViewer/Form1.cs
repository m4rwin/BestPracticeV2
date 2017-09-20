using System;
using System.Windows.Forms;

namespace EventLogViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            eventLog1.Log = "Application";
            eventLog1.EnableRaisingEvents = true;
            eventLog1.MachineName = ".";
            eventLog1.Source = "WindowsFormsAppliccation1";
            eventLog1.EntryWritten += eventLog1_EntryWritten;
        }

        protected void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {
            DateTime time = e.Entry.TimeGenerated;
            string message = e.Entry.Message;

            try
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    this.listBox1.Items.Add(time + " " + message);
                }));
            }
            catch (Exception) { return; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            eventLog1.WriteEntry("toto je zkouska", System.Diagnostics.EventLogEntryType.Information);
        }
    }
}
