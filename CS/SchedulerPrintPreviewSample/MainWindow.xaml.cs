using System;
using System.Windows;
using DevExpress.Xpf.Printing;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Reporting;
using DevExpress.Xpf.Scheduler.Reporting;

namespace SchedulerPrintPreviewSample {
    public partial class MainWindow : Window {

        SchedulerPrintingSettings printingSettings = new SchedulerPrintingSettings();

        public MainWindow() {
            InitializeComponent();

            // Bind the scheduler to data.
            CarsDBDataSet dataSet = new CarsDBDataSet();

            scheduler.Storage.AppointmentStorage.DataSource = dataSet.CarScheduling;
            CarsDBDataSetTableAdapters.CarSchedulingTableAdapter tableAdapter = 
                                            new CarsDBDataSetTableAdapters.CarSchedulingTableAdapter();
            tableAdapter.Fill(dataSet.CarScheduling);

            scheduler.Storage.ResourceStorage.DataSource = dataSet.Cars;
            CarsDBDataSetTableAdapters.CarsTableAdapter carsAdapter = 
                                            new CarsDBDataSetTableAdapters.CarsTableAdapter();
            carsAdapter.Fill(dataSet.Cars);

            // Set the scheduler start date.
            scheduler.Start = new System.DateTime(2010, 7, 15, 0, 0, 0, 0);

            // Specify the time inteval and start day of week used by the print adapter to create a report.
            printAdapter.TimeInterval =
                                new TimeInterval(new DateTime(2010, 7, 15), new DateTime(2010, 7, 30));
            printAdapter.FirstDayOfWeek = FirstDayOfWeek.Wednesday;

            // Specify required printing settings to be passed 
            // to the SchedulerPrintHelper.ShowPrintPreview method that is called on a button click.
            printingSettings.ReportInstance = new XtraSchedulerReport();
            printingSettings.SchedulerPrintAdapter = printAdapter;
            printingSettings.ReportTemplatePath = "WeeklyStyle.schrepx";
            
        }

        private void bthPreview_Click(object sender, RoutedEventArgs e) {
            // Preview the report.
            SchedulerReportConfigurator configurator = new SchedulerReportConfigurator();
            configurator.Configure(printingSettings);
            PrintHelper.ShowPrintPreviewDialog(this, printingSettings.ReportInstance);
        }
    }
}
