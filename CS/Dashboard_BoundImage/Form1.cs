using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraEditors;

namespace Dashboard_BoundImage {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();
            Dashboard dashboard = new Dashboard();

            XmlFileConnectionParameters xmlParams = new XmlFileConnectionParameters();
            xmlParams.FileName = @"..\..\Data\DashboardProductDetails.xml";

            DashboardSqlDataSource xmlDataSource = new DashboardSqlDataSource("Data Source 1", xmlParams);
            SelectQuery selectQuery = SelectQueryFluentBuilder
                .AddTable("Products")
                .SelectColumns("Id", "Name", "Description")
                .Build("Query 1");
            xmlDataSource.Queries.Add(selectQuery);
            xmlDataSource.Fill();
            dashboard.DataSources.Add(xmlDataSource);

            BoundImageDashboardItem boundImage = new BoundImageDashboardItem();
            boundImage.DataSource = xmlDataSource; boundImage.DataMember = "Query 1";
            boundImage.DataBindingMode = ImageDataBindingMode.Uri;
            boundImage.ImageDimension = new Dimension("Name");
            boundImage.UriPattern = @"..\..\ProductDetailsImages\{0}.jpg";
            boundImage.SizeMode = ImageSizeMode.Stretch;

            ListBoxDashboardItem comboBox = new ListBoxDashboardItem();
            comboBox.ShowCaption = false;
            comboBox.DataSource = xmlDataSource; comboBox.DataMember = "Query 1";
            comboBox.FilterDimensions.Add(new Dimension("Name"));
            comboBox.ListBoxType = ListBoxDashboardItemType.Radio;
            comboBox.ShowAllValue = false;

            dashboard.Items.AddRange(comboBox, boundImage);
            dashboardViewer1.Dashboard = dashboard;
        }
    }
}
