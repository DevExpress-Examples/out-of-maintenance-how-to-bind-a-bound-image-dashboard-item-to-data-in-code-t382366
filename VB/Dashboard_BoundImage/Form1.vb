Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraEditors

Namespace Dashboard_BoundImage
	Partial Public Class Form1
		Inherits XtraForm

		Public Sub New()
			InitializeComponent()
			Dim dashboard As New Dashboard()

			Dim xmlParams As New XmlFileConnectionParameters()
			xmlParams.FileName = "..\..\Data\DashboardProductDetails.xml"

			Dim xmlDataSource As New DashboardSqlDataSource("Data Source 1", xmlParams)
			Dim selectQuery As SelectQuery = SelectQueryFluentBuilder.AddTable("Products").SelectColumns("Id", "Name", "Description").Build("Query 1")
			xmlDataSource.Queries.Add(selectQuery)
			xmlDataSource.Fill()
			dashboard.DataSources.Add(xmlDataSource)

			Dim boundImage As New BoundImageDashboardItem()
			boundImage.DataSource = xmlDataSource
			boundImage.DataMember = "Query 1"
			boundImage.DataBindingMode = ImageDataBindingMode.Uri
			boundImage.ImageDimension = New Dimension("Name")
			boundImage.UriPattern = "..\..\ProductDetailsImages\{0}.jpg"
			boundImage.SizeMode = ImageSizeMode.Stretch

			Dim comboBox As New ListBoxDashboardItem()
			comboBox.ShowCaption = False
			comboBox.DataSource = xmlDataSource
			comboBox.DataMember = "Query 1"
			comboBox.FilterDimensions.Add(New Dimension("Name"))
			comboBox.ListBoxType = ListBoxDashboardItemType.Radio
			comboBox.ShowAllValue = False

			dashboard.Items.AddRange(comboBox, boundImage)
			dashboardViewer1.Dashboard = dashboard
		End Sub
	End Class
End Namespace
