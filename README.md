# FlightNet user exporter
The small Windows desktop application exports users of FlightNet into an CSV file for importing into FlightBox. It uses the web interface https://www.flightnet.aero/api/getusers.ashx to get the users. The CSV file can be saved somewhere local on the desktop. Afterwards this CSV can be used to upload the users details into the FlightBox application located on https://lszk-prod.firebaseapp.com/.

### Getting started
1. Open Solution in Visual Studio 2017
2. Build solution
3. Set the `ApiUserName`, `ApiPassword` and `FlightNetCompanyName` within `app.config` file. May the Api user needs to be created in FlightNet first.
May change the `DefaultExportFileName` value for another default destination in `app.config` file.
4. Run the application

### Deployment
Just copy the FlightNetUserExporter.exe and the config file (FlightNetUserExporter.exe.config) to the destination computer and run it from there. 
#### Requirements
- .NET Framework must be installed on it
- Windows based PC
