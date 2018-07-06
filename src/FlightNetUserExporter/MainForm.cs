using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightNetUserExporter.Properties;

namespace FlightNetUserExporter
{
    public partial class MainForm : Form
    {
        delegate void StringArgReturningVoidDelegate(string text);
        private FlightNetUserExporter _exporter;
        
        public MainForm()
        {
            InitializeComponent();
            textBoxFileExport.Text = Settings.Default.DefaultExportFileName;
        }

        private void buttonFileExportBrowser_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Comma separated file|*.csv";
            saveFileDialog.Title = "Export file";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFileExport.Text = saveFileDialog.FileName;
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxLog.Clear();
                var username = Settings.Default.ApiUserName;
                var password = Settings.Default.ApiPassword;
                var company = Settings.Default.FlightNetCompanyName;
            
                _exporter = new FlightNetUserExporter(Settings.Default.FlightNetUserExportApiUri,
                    username, password, company, textBoxFileExport.Text);
                _exporter.ExportFinished += ExporterOnExportFinished;
                _exporter.LogEventRaised += ExporterOnLogEventRaised;
                Thread t = new Thread(new ThreadStart(RunExport));
                // start the thread using the t-variable:
                t.Start();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Fehler beim Exportieren: {exception.Message}", "Fehler", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RunExport()
        {
            _exporter.ExportFlightNetUsers();
        }

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the  
            // calling thread to the thread ID of the creating thread.  
            // If these threads are different, it returns true.  
            if (textBoxLog.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(SetText);
                Invoke(d, new object[] { text });
            }
            else
            {
                textBoxLog.Text += text;
            }
        }

        private void ExporterOnLogEventRaised(object sender, LogEventArgs logEventArgs)
        {
            SetText($"{logEventArgs.Text}{Environment.NewLine}");
        }
        
        private void ExporterOnExportFinished(object sender, EventArgs eventArgs)
        {
            _exporter.ExportFinished -= ExporterOnExportFinished;
            _exporter.LogEventRaised -= ExporterOnLogEventRaised;

            if (_exporter.HasExportError)
            {
                MessageBox.Show($"Fehler beim Exportieren der Benutzerdaten aus FlightNet.{Environment.NewLine}{Environment.NewLine}Fehler-Meldung:{Environment.NewLine}{_exporter.ExportErrorMessage}", "Export-Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Benutzerdaten erfolgreich aus FlightNet exportiert.", "Export fertiggestellt",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
