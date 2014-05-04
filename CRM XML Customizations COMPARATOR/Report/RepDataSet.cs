namespace CustomizationsComparator.Report {

    partial class RepDataSet
    {
        public class DataTable
        {
            
            public static string Name
            {
                get { return (new RepDataSet()).tableReportDataTable.TableName; }
            }

            public static string ColumnElementName
            {
                get { return (new RepDataSet()).tableReportDataTable.Element_NameColumn.ColumnName; }
            }

            public static string ColumnPropertyName
            {
                get { return (new RepDataSet()).tableReportDataTable.Property_NameColumn.ColumnName; }
            }

            public static string ColumnFirstValue
            {
                get { return (new RepDataSet()).tableReportDataTable.First_ValueColumn.ColumnName; }
            }

            public static string ColumnSecondValue
            {
                get { return (new RepDataSet()).tableReportDataTable.Second_ValueColumn.ColumnName; }
            }

        }
    }
}
