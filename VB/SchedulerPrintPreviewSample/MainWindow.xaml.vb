Imports System
Imports System.Windows
Imports DevExpress.Xpf.Printing
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Reporting
Imports DevExpress.Xpf.Scheduler.Reporting

Namespace SchedulerPrintPreviewSample
    Partial Public Class MainWindow
        Inherits Window

        Private printingSettings As New SchedulerPrintingSettings()

        Public Sub New()
            InitializeComponent()

            ' Bind the scheduler to data.
            Dim dataSet As New CarsDBDataSet()

            scheduler.Storage.AppointmentStorage.DataSource = dataSet.CarScheduling
            Dim tableAdapter As New CarsDBDataSetTableAdapters.CarSchedulingTableAdapter()
            tableAdapter.Fill(dataSet.CarScheduling)

            scheduler.Storage.ResourceStorage.DataSource = dataSet.Cars
            Dim carsAdapter As New CarsDBDataSetTableAdapters.CarsTableAdapter()
            carsAdapter.Fill(dataSet.Cars)

            ' Set the scheduler start date.
            scheduler.Start = New Date(2010, 7, 15, 0, 0, 0, 0)

            ' Specify the time inteval and start day of week used by the print adapter to create a report.
            printAdapter.TimeInterval = New TimeInterval(New Date(2010, 7, 15), New Date(2010, 7, 30))
            printAdapter.FirstDayOfWeek = FirstDayOfWeek.Wednesday

            ' Specify required printing settings to be passed 
            ' to the SchedulerPrintHelper.ShowPrintPreview method that is called on a button click.
            printingSettings.ReportInstance = New XtraSchedulerReport()
            printingSettings.SchedulerPrintAdapter = printAdapter
            printingSettings.ReportTemplatePath = "WeeklyStyle.schrepx"

        End Sub

        Private Sub bthPreview_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ' Preview the report.
            Dim configurator As New SchedulerReportConfigurator()
            configurator.Configure(printingSettings)
            PrintHelper.ShowPrintPreviewDialog(Me, printingSettings.ReportInstance)
        End Sub
    End Class
End Namespace
