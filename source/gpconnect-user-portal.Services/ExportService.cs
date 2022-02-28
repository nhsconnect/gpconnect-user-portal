using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using gpconnect_user_portal.DAL.Resources;
using gpconnect_user_portal.DTO.Request;
using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services
{
    public class ExportService : IExportService
    {
        private string _reportName;
        private readonly ResourceManager _resourceManager; 
        private readonly IQueryService _queryService;

        public ExportService(IQueryService queryService)
        {
            _queryService = queryService; 
            _resourceManager = new ResourceManager("gpconnect_user_portal.DAL.Resources.ReportFieldNameResources", typeof(ReportFieldNameResources).Assembly);
        }

        public async Task<DataTable> GetSitesForExport(SearchRequest searchRequest = null)
        {
            var sites = await _queryService.GetSites(searchRequest);
            var json = sites.SearchResultEntries.ConvertObjectToJsonData();
            var dataTable = json.ConvertJsonDataToDataTable();
            return ManipulateExportedColumns(dataTable);
        }

        public MemoryStream CreateReport(DataTable result, string reportName = "")
        {
            var memoryStream = new MemoryStream();
            var spreadsheetDocument = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook);
            var workbookPart = spreadsheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();
            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

            var sheetData = new SheetData();

            WorkbookStylesPart workbookStylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
            workbookStylesPart.Stylesheet = StyleSheetBuilder.CreateStylesheet();
            workbookStylesPart.Stylesheet.Save();

            var workSheet = new Worksheet();

            var columns = BuildColumns(result);
            workSheet.Append(columns);
            workSheet.Append(sheetData);

            worksheetPart.Worksheet = workSheet;

            _reportName = StringExtensions.Coalesce(_reportName, reportName);

            var sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
            var sheet = new Sheet
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = _reportName
            };

            sheets.AppendChild(sheet);

            BuildWorksheetHeader(sheetData);
            BuildHeaderRow(sheetData, result.Columns);
            BuildDataRows(sheetData, result.Rows);

            workbookPart.Workbook.Save();
            spreadsheetDocument.Close();

            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        public FileStreamResult GetFileStream(MemoryStream memoryStream, string fileName = null)
        {
            return new FileStreamResult(memoryStream, new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
            {
                FileDownloadName = fileName ?? $"{DateTime.UtcNow.ToFileTimeUtc()}.xlsx"
            };
        }

        private Columns BuildColumns(DataTable result)
        {
            var columns = new Columns();
            var dataRow = result.Rows[0];
            for (var i = 0; i < result.Columns.Count; i++)
            {
                var maxColumnLength = result.AsEnumerable().Max(row => row.Field<object>(result.Columns[i].ColumnName)?.ToString()?.Length);
                var columnNameLength = result.Columns[i].ColumnName.Length;
                var col = new Column
                {
                    CustomWidth = true,
                    Width = maxColumnLength < columnNameLength ? columnNameLength * 2 : maxColumnLength.GetValueOrDefault(),
                    Min = Convert.ToUInt32(i + 1),
                    Max = Convert.ToUInt32(i + 1)
                };
                columns.AppendChild(col);
            }
            return columns;
        }

        private void BuildDataRows(SheetData sheetData, DataRowCollection dataRowCollection)
        {
            for (var i = 0; i < dataRowCollection.Count; i++)
            {
                var row = new Row();
                var dataRow = dataRowCollection[i];

                for (var j = 0; j < dataRow.ItemArray.Length; j++)
                {
                    var cellValue = dataRow.ItemArray[j]?.ToString().Trim().StringToYesNo();
                    var cell = new Cell
                    {
                        DataType = cellValue.GetCellDataType(),
                        CellValue = cellValue?.Length > 0 ? new CellValue(cellValue) : null
                    };
                    row.AppendChild(cell);
                }
                sheetData.AppendChild(row);
            }
        }

        private void BuildHeaderRow(SheetData sheetData, DataColumnCollection dataColumnCollection)
        {
            var headerRow = new Row();
            for (var j = 0; j < dataColumnCollection.Count; j++)
            {
                var column = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(GetColumnName(dataColumnCollection[j].ColumnName)),
                    StyleIndex = 2
                };
                headerRow.AppendChild(column);
            }
            sheetData.AppendChild(headerRow);
        }

        private string GetColumnName(string columnName)
        {
            _resourceManager.IgnoreCase = true;
            return StringExtensions.Coalesce(_resourceManager.GetString(columnName), columnName);
        }

        private void BuildWorksheetHeader(SheetData sheetData)
        {
            var row1 = new Row { Height = 55 };
            var titleCell = new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue(_reportName),
                StyleIndex = 1
            };

            row1.AppendChild(titleCell);
            sheetData.AppendChild(row1);

            var row2 = new Row { Height = 40 };
            var subTitleCell = new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue($"Report generated at {DateTime.Now:F}"),
                StyleIndex = 3
            };
            row2.AppendChild(subTitleCell);
            sheetData.AppendChild(row2);

            var row3 = new Row { Height = 30 };
            sheetData.AppendChild(row3);
        }

        private DataTable ManipulateExportedColumns(DataTable dataTable)
        {
            var lastColumnIndex = dataTable.Columns.Count - 1;
            AddDataRows(dataTable, lastColumnIndex, AddColumns(dataTable));
            return dataTable;
        }

        private void AddDataRows(DataTable dataTable, int lastColumnAddedIndex, Dictionary<string, string> appendedDataColumns)
        {
            for (var k = 0; k < dataTable.Rows.Count; k++)
            {   
                foreach(KeyValuePair<string, string> appendedDataColumn in appendedDataColumns)
                {
                    dataTable.Rows[k].SetField(appendedDataColumn.Key, appendedDataColumn.Value);
                }
            }
            dataTable.AcceptChanges();
        }

        private Dictionary<string, string> AddColumns(DataTable dataTable)
        {
            var firstDataRow = dataTable.Rows[0];
            var dataRowCount = dataTable.Rows.Count;
            var appendedDataColumns = new Dictionary<string, string>();
            var deletedDataColumns = new List<int>();

            for (var i = 0; i < dataTable.Columns.Count; i++)
            {               
                if (firstDataRow[i].GetType() == typeof(string[]))
                {
                    for (var j = 0; j < ((string[])firstDataRow[i]).Length; j++)
                    {
                        var columnName = ((string[])firstDataRow[i])[j].Split(":")?[0];
                        appendedDataColumns.Add(columnName, ((string[])firstDataRow[i])[j].Split(":")?[1]);
                        dataTable.Columns.Add(new DataColumn(columnName));
                    }
                    deletedDataColumns.Add(i);
                }                
            }
            foreach (var columnIndex in deletedDataColumns)
            {
                dataTable.Columns.RemoveAt(columnIndex);
            }
            dataTable.AcceptChanges();
            return appendedDataColumns;
        }
    }
}
