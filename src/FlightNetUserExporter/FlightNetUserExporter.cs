using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightNetUserExporter
{
    public class FlightNetUserExporter
    {
        public event EventHandler<LogEventArgs> LogEventRaised; 
        public event EventHandler ExportFinished;

        public string ExportFileName { get; set; }

        public string FlightNetUserExportApiUri { get; set; }

        public string ApiUserName { get; set; }

        public string ApiPassword { get; set; }

        public string FlightNetCompanyName { get; set; }

        public bool HasExportError { get; set; }

        public string ExportErrorMessage { get; set; }


        public FlightNetUserExporter(string flightNetUserExportApiUri, string apiUserName, string apiPassword,
            string flightNetCompanyName, string exportFileName)
        {
            FlightNetUserExportApiUri = flightNetUserExportApiUri;
            ApiUserName = apiUserName;
            ApiPassword = apiPassword;
            FlightNetCompanyName = flightNetCompanyName;
            ExportFileName = exportFileName;
        }
        
        public void ExportFlightNetUsers()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    OnLogEventRaised("Verbinde mit FlightNet...");
                    Thread.Sleep(50);
                    //http://www.flightnet.aero/manual/index.html?abouttheapi.htm
                    var uri = "https://www.flightnet.aero/api/getusers.ashx";
                    client.BaseAddress = new Uri("https://www.flightnet.aero/api/");

                    var apiUri =
                        $"getusers.ashx?company={FlightNetCompanyName}&username={ApiUserName}&password={ApiPassword}&includedeleted=false";
                    var request = client.GetAsync(apiUri)
                        .ContinueWith(res =>
                        {
                            OnLogEventRaised("Lade Daten...");
                            Thread.Sleep(50);
                            var result = res.Result;
                            if (result.IsSuccessStatusCode)
                            {
                                Thread.Sleep(50);
                                OnLogEventRaised("Daten erfolgreich heruntergeladen. Exportiere Daten...");

                                var readData = result.Content.ReadAsStringAsync();
                                readData.Wait();

                                var array = readData.Result.Split(new string[] { Environment.NewLine },
                                    StringSplitOptions.RemoveEmptyEntries);

                                var correctNumberOfColumns = 31;
                                List<string> headers = new List<string>();
                                var indexUserName = -1;
                                var indexLastName = -1;
                                var indexFirstName = -1;
                                var indexPhoneMobile = -1;
                                var numberOfLines = 0;

                                using (var w = new StreamWriter(ExportFileName, false, Encoding.UTF8))
                                {
                                    foreach (var line in array)
                                    {
                                        char[] delimiter = new char[] {'\t'};
                                        var splittedLine = line.Split(delimiter).ToList();

                                        if (splittedLine.Count() == 2)
                                        {
                                            if (splittedLine[0] == "206")
                                            {
                                                HasExportError = true;
                                                ExportErrorMessage = splittedLine[1];
                                                OnLogEventRaised($"Fehler beim Exportieren!");
                                                OnLogEventRaised($"Fehler: {ExportErrorMessage}");
                                                ExportFinished?.Invoke(this, EventArgs.Empty);
                                                break;
                                            }
                                        }

                                        if (splittedLine.FindIndex(x => x.StartsWith("UserName")) >= 0
                                            && splittedLine.FindIndex(x => x.StartsWith("LastName")) >= 0
                                            && splittedLine.FindIndex(x => x.StartsWith("FirstName")) >= 0
                                            && splittedLine.FindIndex(x => x.StartsWith("PhoneMobile")) >= 0)
                                        {
                                            headers = splittedLine;
                                            correctNumberOfColumns = splittedLine.Count;
                                            w.WriteLine("UserName,LastName,FirstName,PhoneMobile");
                                            indexUserName = headers.FindIndex(x => x.StartsWith("UserName"));
                                            indexLastName = headers.FindIndex(x => x.StartsWith("LastName"));
                                            indexFirstName = headers.FindIndex(x => x.StartsWith("FirstName"));
                                            indexPhoneMobile = headers.FindIndex(x => x.StartsWith("PhoneMobile"));
                                            continue;
                                        }

                                        if (splittedLine.Count == correctNumberOfColumns
                                            && headers.Count == correctNumberOfColumns)
                                        {
                                            var username = splittedLine[indexUserName];
                                            var lastname = splittedLine[indexLastName];
                                            var firstname = splittedLine[indexFirstName];
                                            var phone = splittedLine[indexPhoneMobile];

                                            w.WriteLine($"{username},{lastname},{firstname},{phone}");
                                            numberOfLines++;
                                        }
                                    }

                                    w.Flush();

                                    if (numberOfLines == 0)
                                    {
                                        HasExportError = true;
                                        ExportErrorMessage = "Keine Datensätze exportiert. Allenfalls fehlerhafte Verbindungsdaten zu FlightNet.";
                                    }

                                    OnLogEventRaised($"{numberOfLines} Datensätze aus FlightNet exportiert.");
                                    ExportFinished?.Invoke(this, EventArgs.Empty);
                                }
                            }
                            else
                            {
                                OnLogEventRaised("Fehler in der Verbindung zu FlightNet. Kann Daten nicht exportieren.");
                            }
                        });

                    request.Wait();
                }
            }
            catch (Exception e)
            {
                HasExportError = true;
                ExportErrorMessage = e.Message;
                OnLogEventRaised("Fehler beim Exportieren..." + Environment.NewLine + "Fehlermeldung: " + e.Message);
                ExportFinished?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnLogEventRaised(string text)
        {
            LogEventRaised?.Invoke(this, new LogEventArgs(text));
        }
    }
}
