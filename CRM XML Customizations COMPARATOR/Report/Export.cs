using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace CustomizationsComparator.Report
{
    public class Export
    {
        public enum ExportFormat : int { Text = 0, CSV = 1, Excel = 2 }; // Export format enumeration					

        public Export()
        {

        }

        public void ExportDetails(List<string> elems, ExportFormat formatType, string fileName)
        {
            //if (elems.Count > 0)

            if (formatType == ExportFormat.Text) //1. Export to .TXT
            {
                var sb = new StringBuilder();
                foreach (string elem in elems)
                    sb.Append(elem + "\r\n");
                var sr = new StreamWriter(fileName);
                sr.Write(sb);
                sr.Close();
            }
            else //2. Export to Excel, Or CSV
            {
                string colName = "Absent";
                // Create Dataset
                var dsExport = new DataSet("Export");
                dsExport.Tables.Add("Values");
                dsExport.Tables[0].Columns.Add(colName);
                foreach (string elem in elems)
                {
                    DataRow row = dsExport.Tables[0].NewRow();
                    row["Absent"] = elem;
                    dsExport.Tables[0].Rows.Add(row);
                }

                Export_XSLT_Windows(dsExport, new string[] { colName }, new string[] { colName }, formatType, fileName);
            }
        }



        /// <summary>
        /// To get the specified column headers in the datatable and	
        //			   exorts in CSV / Excel format with specified columns and 
        //			   with specified headers
        /// </summary>
        /// <param name="DetailsTable"></param>
        /// <param name="ColumnList"></param>
        /// <param name="Headers"></param>
        /// <param name="FormatType"></param>
        /// <param name="FileName"></param>
        public void ExportDetails(DataTable detailsTable, string[] headers, ExportFormat formatType,
                    string fileName)
        {
            try
            {
                if (detailsTable.Rows.Count == 0)
                    throw new Exception("There are no details to export");
                DataSet dsExport = new DataSet("Export");
                DataTable dtExport = detailsTable.Copy();
                dtExport.TableName = "Values";
                dsExport.Tables.Add(dtExport);

                if (formatType == ExportFormat.Text) //1. Export to .TXT
                {
                    var sb = new StringBuilder();

                    foreach (DataTable tbl in dsExport.Tables)
                    {
                        foreach (DataRow row in tbl.Rows)
                        {
                            if (!String.IsNullOrEmpty(row[RepDataSet.DataTable.ColumnElementName].ToString()))
                            {
                                sb.Append("\r\n\r\n");
                                sb.Append("===========================================================================");
                                sb.Append("\r\n\r\n" + row[RepDataSet.DataTable.ColumnElementName] + "\r\n");
                                sb.Append("===========================================================================");
                            }
                            sb.Append("\r\n              " + row[RepDataSet.DataTable.ColumnPropertyName] + "\r\n");
                            sb.Append("                   (1):  " + row[RepDataSet.DataTable.ColumnFirstValue].ToString() + "\r\n");
                            sb.Append("                   (2):  " + row[RepDataSet.DataTable.ColumnSecondValue].ToString() + "\r\n");
                        }

                    }


                    var sr = new StreamWriter(fileName);
                    sr.Write(sb);
                    sr.Close();
                }
                else //2. Export to Excel, Or CSV
                {
                    var sFields = new string[headers.Length];

                    for (int i = 0; i < headers.Length; i++)
                        sFields[i] = ReplaceSpclChars(dtExport.Columns[i].ColumnName);

                    Export_XSLT_Windows(dsExport, headers, sFields, formatType, fileName);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Exports dataset into CSV / Excel format
        /// </summary>
        /// <param name="dsExport"></param>
        /// <param name="sHeaders"></param>
        /// <param name="sFields"></param>
        /// <param name="FormatType"></param>
        /// <param name="FileName"></param>
        private void Export_XSLT_Windows(DataSet dsExport, string[] sHeaders, string[] sFields, ExportFormat FormatType, string FileName)
        {
            try
            {
                // XSLT to use for transforming this dataset.						
                using (var stream = new MemoryStream())
                {
                    using (var writer = new XmlTextWriter(stream, Encoding.UTF8))
                    {
                        CreateStylesheet(writer, sHeaders, sFields, FormatType);
                        writer.Flush();
                        stream.Seek(0, SeekOrigin.Begin);

                        var xmlDoc = new XmlDataDocument(dsExport.Copy());
                        var xslTran = new XslTransform();
                        xslTran.Load(new XmlTextReader(stream), null, null);

                        var sw = new StringWriter();
                        xslTran.Transform(xmlDoc, null, sw, null);

                        //write out the Content									
                        using (var strwriter = new StreamWriter(FileName))
                        {
                            strwriter.WriteLine(sw.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CreateStylesheet(XmlTextWriter writer, string[] sHeaders, string[] sFileds, ExportFormat FormatType)
        {
            try
            {
                // xsl:stylesheet
                string ns = "http://www.w3.org/1999/XSL/Transform";
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("xsl", "stylesheet", ns);
                writer.WriteAttributeString("version", "1.0");
                writer.WriteStartElement("xsl:output");
                writer.WriteAttributeString("method", "text");
                writer.WriteAttributeString("version", "4.0");
                writer.WriteEndElement();

                // xsl-template
                writer.WriteStartElement("xsl:template");
                writer.WriteAttributeString("match", "/");

                // xsl:value-of for headers
                for (int i = 0; i < sHeaders.Length; i++)
                {
                    writer.WriteString("\"");
                    writer.WriteStartElement("xsl:value-of");
                    writer.WriteAttributeString("select", "'" + sHeaders[i] + "'");
                    writer.WriteEndElement(); // xsl:value-of
                    writer.WriteString("\"");
                    if (i != sFileds.Length - 1) writer.WriteString((FormatType == ExportFormat.CSV) ? "," : "	");
                }

                // xsl:for-each
                writer.WriteStartElement("xsl:for-each");
                writer.WriteAttributeString("select", "Export/Values");
                writer.WriteString("\r\n");

                // xsl:value-of for data fields
                for (int i = 0; i < sFileds.Length; i++)
                {
                    writer.WriteString("\"");
                    writer.WriteStartElement("xsl:value-of");
                    writer.WriteAttributeString("select", sFileds[i]);
                    writer.WriteEndElement(); // xsl:value-of
                    writer.WriteString("\"");
                    if (i != sFileds.Length - 1) writer.WriteString((FormatType == ExportFormat.CSV) ? "," : "	");
                }

                writer.WriteEndElement(); // xsl:for-each
                writer.WriteEndElement(); // xsl-template
                writer.WriteEndElement(); // xsl:stylesheet
                writer.WriteEndDocument();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Replaces special characters with XML codes 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private string ReplaceSpclChars(string fieldName)
        {
            fieldName = fieldName.Replace(" ", "_x0020_");	//			space 	-> 	_x0020_
            fieldName = fieldName.Replace("%", "_x0025_");	//			%		-> 	_x0025_
            fieldName = fieldName.Replace("#", "_x0023_");		//			#		->	_x0023_
            fieldName = fieldName.Replace("&", "_x0026_");//			&		->	_x0026_
            fieldName = fieldName.Replace("/", "_x002F_");	//			/		->	_x002F_
            return fieldName;
        }


    }
}

/*
/// <summary>
// To get all the column headers in the datatable and 
//			   exorts in CSV / Excel format with all columns   
/// </summary>
/// <param name="DetailsTable"></param>
/// <param name="FormatType"></param>
/// <param name="FileName"></param>
public void ExportDetails(DataTable DetailsTable, ExportFormat FormatType, string FileName)
{
    try
    {				
        if(DetailsTable.Rows.Count == 0) 
            throw new Exception("There are no details to export.");				
				
        // Create Dataset
        DataSet dsExport = new DataSet("Export");
        DataTable dtExport = DetailsTable.Copy();
        dtExport.TableName = "Values"; 
        dsExport.Tables.Add(dtExport);	
				
        // Getting Field Names
        string[] sHeaders = new string[dtExport.Columns.Count];
        string[] sFields = new string[dtExport.Columns.Count];
				
        for (int i=0; i < dtExport.Columns.Count; i++)
        {
            //sHeaders[i] = ReplaceSpclChars(dtExport.Columns[i].ColumnName);
            sHeaders[i] = dtExport.Columns[i].ColumnName;
            sFields[i] = ReplaceSpclChars(dtExport.Columns[i].ColumnName);					
        }

        if (FormatType == ExportFormat.Text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string field in sFields)
                sb.Append(field + "\r\n");

        }
        else
        {
            Export_with_XSLT_Windows(dsExport, sHeaders, sFields, FormatType, FileName);
        }
    }			
    catch(Exception Ex)
    {
        throw Ex;
    }			
}

/// <summary>
/// To get the specified column headers in the datatable and
//			   exorts in CSV / Excel format with specified columns
/// </summary>
/// <param name="DetailsTable"></param>
/// <param name="ColumnList"></param>
/// <param name="FormatType"></param>
/// <param name="FileName"></param>
public void ExportDetails(DataTable DetailsTable, int[] ColumnList, ExportFormat FormatType, string FileName)
{
    try
    {
        if(DetailsTable.Rows.Count == 0)
            throw new Exception("There are no details to export");
				
        // Create Dataset
        DataSet dsExport = new DataSet("Export");
        DataTable dtExport = DetailsTable.Copy();
        dtExport.TableName = "Values"; 
        dsExport.Tables.Add(dtExport);

        if(ColumnList.Length > dtExport.Columns.Count)
            throw new Exception("ExportColumn List should not exceed Total Columns");
				
        // Getting Field Names
        string[] sHeaders = new string[ColumnList.Length];
        string[] sFields = new string[ColumnList.Length];
				
        for (int i=0; i < ColumnList.Length; i++)
        {
            if((ColumnList[i] < 0) || (ColumnList[i] >= dtExport.Columns.Count))
                throw new Exception("ExportColumn Number should not exceed Total Columns Range");
					
            sHeaders[i] = dtExport.Columns[ColumnList[i]].ColumnName;
            sFields[i] = ReplaceSpclChars(dtExport.Columns[ColumnList[i]].ColumnName);					
        }
					
		        
        if (FormatType == ExportFormat.Text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string field in sFields)
                sb.Append(field + "\r\n");

        }
        else
            Export_with_XSLT_Windows(dsExport, sHeaders, sFields, FormatType, FileName);

    }			
    catch(Exception Ex)
    {
        throw Ex;
    }			
}        

 /// <summary>
/// To get the specified column headers in the datatable and	
//			   exorts in CSV / Excel format with specified columns and 
//			   with specified headers
/// </summary>
/// <param name="DetailsTable"></param>
/// <param name="ColumnList"></param>
/// <param name="Headers"></param>
/// <param name="FormatType"></param>
/// <param name="FileName"></param>
public void ExportDetails2(DataTable DetailsTable, 
            int[] ColumnList, string[] Headers, ExportFormat FormatType, 
            string FileName)
{
    try
    {
        if(DetailsTable.Rows.Count == 0)
            throw new Exception("There are no details to export");
				
        // Create Dataset
        DataSet dsExport = new DataSet("Export");
        DataTable dtExport = DetailsTable.Copy();

        if (FormatType == ExportFormat.Text) //1. Export to .TXT
        {
            StringBuilder sb = new StringBuilder();
           // foreach (string field in sFields)
            //    sb.Append(field + "\r\n");
            StreamWriter sr = new StreamWriter(FileName);
            sr.Write(sb);
            sr.Close();
        }
        else //2. Export to Excel, Or CSV
        {
            dtExport.TableName = "Values";
            dsExport.Tables.Add(dtExport);

            if (ColumnList.Length != Headers.Length)
                throw new Exception("ExportColumn List and Headers List should be of same length");
            else if (ColumnList.Length > dtExport.Columns.Count || Headers.Length > dtExport.Columns.Count)
                throw new Exception("ExportColumn List should not exceed Total Columns");

            // Getting Field Names
            string[] sFields = new string[ColumnList.Length];

            for (int i = 0; i < ColumnList.Length; i++)
            {
                if ((ColumnList[i] < 0) || (ColumnList[i] >= dtExport.Columns.Count))
                    throw new Exception("ExportColumn Number should not exceed Total Columns Range");

                sFields[i] = ReplaceSpclChars(dtExport.Columns[ColumnList[i]].ColumnName);
            }

            Export_with_XSLT_Windows(dsExport, Headers, sFields, FormatType, FileName);
        }
				
    }			
    catch(Exception Ex)
    {
        throw Ex;
    }			
}*/