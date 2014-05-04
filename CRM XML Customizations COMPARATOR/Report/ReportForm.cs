using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace CustomizationsComparator.Report
{
    public class ReportForm
    {
        List<OriginalElement> originalElems = new List<OriginalElement>();
        List<OriginalElement> newElems = new List<OriginalElement>();

        DataSet _reportDS;
        public DataSet DataSource
        {
            get { return _reportDS; }
            set { _reportDS = value; }
        }

        public ReportForm(string headerText, DataSet diffElemsData, IEnumerable<string> originalElems, IEnumerable<string> newElems)
        {         
            //1. Export to Excel
            var fd = new SaveFileDialog();
            fd.Filter = "Excel file(*.xls)|*.xls";
            fd.FileName = headerText;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string filepath = fd.FileName;
                //1.1 Generate columns
                string[] hColumns = new string[4]{
                                    RepDataSet.DataTable.ColumnElementName,
                                    RepDataSet.DataTable.ColumnPropertyName, 
                                    RepDataSet.DataTable.ColumnFirstValue,
                                    RepDataSet.DataTable.ColumnSecondValue};

                //1.2 Export the details of specified columns
                DataTable dt = diffElemsData.Tables[0].Copy();

                //_________Delete similar elems names to indication groups only - for REPORT ONLY
                List<string> groupElems = new List<string>();
                string elementColName = RepDataSet.DataTable.ColumnElementName;
                foreach(DataRow row in dt.Rows)
                {
                    string propertyName = row[elementColName].ToString();
                    if (!groupElems.Contains(propertyName))                    
                        groupElems.Add(propertyName);                    
                    else
                        row[elementColName] = "";                    
                }
                //1.3 Export differ properties
                var export = new Export();               
                export.ExportDetails(dt,  hColumns, Export.ExportFormat.Excel, filepath);

                //1.2 Export original elements 
                //export.ExportDetails(originalElems, new int[1] { 0 }, new string[1] { " Element name" }, Export.ExportFormat.Excel, filepath);
            }         

            //2.
            this.DataSource = diffElemsData;

            foreach (string name in originalElems)
                this.originalElems.Add(new OriginalElement(name));
            foreach (string name in newElems)
                this.newElems.Add(new OriginalElement(name));                
        }
    }
}

public class ReportHeader
{
    public static string Attributes = "Differ attributes";
    public static string Fields = "Differ fields";
    public static string FormCells = "Differ cells";
    public static string FormEvents = "Differ events";
    public static string FormData = "Differ data";
}


/*
 
public partial class ReportDialog : Form
{
    List<OriginalElement> originalElems = new List<OriginalElement>();
    List<OriginalElement> newElems = new List<OriginalElement>();

    DataSet _reportDS;
    public DataSet DataSource
    {
        get { return _reportDS; }
        set { _reportDS = value; }
    }

    /*
        /// <summary>
        /// Load fields/ attributes report data
        /// </summary>
        /// <param name="diffElemsData"></param>
        /// <param name="originalElems"></param>
        /// <param name="newElems"></param>
        public ReportDialog( string headerText, DataSet diffElemsData, List<string> originalElems, List<string> newElems)
        {
            InitializeComponent();
            //2.
            this.DataSource = diffElemsData;

            foreach (string name in originalElems)
                this.originalElems.Add(new OriginalElement(name));
            foreach (string name in newElems)
                this.newElems.Add(new OriginalElement(name));

            //3. Load Header
            SetHeader(headerText);

            //4.load Report Data
            LoadDataToReport(DifferenceInPropsReport, diffElemsData);//, originalElems, newElems);                  
        }

        private void SetHeader(string caption)
        {
            FieldHeadingObject header = ((FieldHeadingObject)(DifferenceInPropsReport.Section1.ReportObjects["HeaderText"]));
            header.Text = caption;
        }

        private void LoadDataToReport(ReportClass report, DataSet diffElemsDS)//, List<string> originalElems, List<string> newElems)
        {
            // Fill difference report
            if ((diffElemsDS != null) && (diffElemsDS.Tables.Count > 0))
                report.SetDataSource(diffElemsDS.Tables[0]);
            UpdateReportViewer(report);
        }

        private void LoadDataToReport(ReportClass report, List<OriginalElement> diffElemsList)//, List<string> originalElems, List<string> newElems)
        {
            // if ((diffElemsList != null) && (diffElemsList.Count > 0))            
            report.SetDataSource(diffElemsList);
            UpdateReportViewer(report);
        }

        private void UpdateReportViewer(ReportClass report)
        {
            DiffernceReportViewer.ReportSource = report;
            DiffernceReportViewer.DisplayGroupTree = true;
            DiffernceReportViewer.Refresh();
            DiffernceReportViewer.Visible = true;
        }


        private void diffRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                LoadDataToReport(DifferenceInPropsReport, DataSource);
        }

        private void absentRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                LoadDataToReport(OriginalElemsReport, originalElems);
        }

        private void newRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                LoadDataToReport(OriginalElemsReport, newElems);
        }

        private void ReportDialog_Load(object sender, EventArgs e)
        {

        }


    }
}
 */