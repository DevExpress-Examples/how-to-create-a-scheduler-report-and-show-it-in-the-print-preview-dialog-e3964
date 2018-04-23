Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Printing
Imports DevExpress.XtraScheduler.Reporting
Imports DevExpress.Xpf.Scheduler.Reporting

Class MainWindow

    Private printingSettings As New SchedulerPrintingSettings()
    Public Sub New()

        ' This call is required by the designer.
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
        scheduler.Start = New System.DateTime(2010, 7, 15, 0, 0, 0, 0)

        ' Specify the time inteval and start day of week used by the print adapter to create a report.
        printAdapter.TimeInterval = New TimeInterval(New DateTime(2010, 7, 15), New DateTime(2010, 7, 30))
        printAdapter.FirstDayOfWeek = DevExpress.XtraScheduler.FirstDayOfWeek.Wednesday

        ' Specify required printing settings to be passed 
        ' to the SchedulerPrintPreviewCommand.Execute method that is called on a button click.
        printingSettings.ReportInstance = New XtraSchedulerReport()
        printingSettings.SchedulerPrintAdapter = printAdapter
        printingSettings.ReportTemplatePath = "C:\Users\Public\Documents\DevExpress 2012.1 Demos\Components\Data\SchedulerReportTemplates\WeeklyStyle.schrepx"

    End Sub

    Private Sub bthPreview_Click(sender As Object, e As RoutedEventArgs)
        ' Preview the report.
        Dim configurator As New SchedulerReportConfigurator()
        configurator.Configure(printingSettings)
        PrintHelper.ShowPrintPreview(Me, printingSettings.ReportInstance)
    End Sub
End Class
