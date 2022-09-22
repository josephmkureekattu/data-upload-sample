using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace data_processing_fn_app.Extensions
{
    public static class ExcelWorksheetExtensions
    {
        public static List<T> Convert<T>(this ExcelWorksheet worksheet, List<string> columnsToSkip = null)
            where T : new()
        {
            var dataParsed = new List<T>();

            if (worksheet is null)
                return (dataParsed);

            var (headerRowIndex, headerRow) = GetHeaderInfo<T>(worksheet,  columnsToSkip);

            if (headerRowIndex == -1)
                throw new Exception("file parsing failed");

            var cellRange = worksheet.GetUsedCellRange(true);

            for (int j = headerRowIndex + 1; j <= cellRange.LastRowIndex; j++)
            {
                var dataParsedItem = new T();
                var dynamicColumns = new List<KeyValuePair<string, string>>();
                var dynamicColumnsProperty = dataParsedItem.GetType().GetProperties().SingleOrDefault(x => x.PropertyType == typeof(List<KeyValuePair<string, string>>));

                foreach (var cellIndexRequired in headerRow.Keys)
                {
                    var prop = dataParsedItem.GetType().GetProperty(headerRow[cellIndexRequired]);

                    var cell = worksheet.Rows[j].AllocatedCells[cellIndexRequired];
                    if (prop == null)
                    {
                        dynamicColumns.Add(new KeyValuePair<string, string>(headerRow[cellIndexRequired], cell.Value?.ToString()));
                        continue;
                    }
                    SetPropertyValue<T>(prop, dataParsedItem, cell);
                }
                if (dynamicColumnsProperty != null)
                {
                    dynamicColumnsProperty.SetValue(dataParsedItem, dynamicColumns, null);
                }
                dataParsed.Add(dataParsedItem);
            }

            return dataParsed;
        }

        private static (int headerRowIndex, Dictionary<int, string> headerInfo) GetHeaderInfo<T>(ExcelWorksheet worksheet,
            List<string> columnsToSkip = null)
            where T : new()
        {
            // find header row
            Dictionary<int, string> headerRow = new Dictionary<int, string>();
            int headerRowIndex = -1;

            //expect header to be in the first row - allows to validate if any columns are missing.

            //for (int i = 0; i < worksheet.Rows.Count(); i++)
            {
                var dataParsedItem = new T();

                var itemPropertyCount = dataParsedItem.GetType().GetProperties().Count();
                foreach (var prop in dataParsedItem.GetType().GetProperties())
                {
                    var comparingValue = prop.Name;
                    var displayNameAttribute = prop.GetCustomAttributes(true)
                        .FirstOrDefault(ca => ca.GetType().Name.Equals("DisplayNameAttribute"));

                    if (displayNameAttribute != null)
                        comparingValue = (displayNameAttribute as System.ComponentModel.DisplayNameAttribute).DisplayName;

                    var temp = worksheet.Rows[0].Cells.FirstOrDefault(c => c.Value != null && c.Value.ToString().Trim().Equals(comparingValue));

                    if (temp != null)
                    {
                        // display name can't be added here because this value will be used below for comparison
                        headerRow.Add(temp.Column.Index, prop.Name);
                        headerRowIndex = 0;
                        continue;
                    }

                    var index = 0;
                    foreach (var cell in worksheet.Rows[0].AllocatedCells)
                    {
                        if ((string.IsNullOrWhiteSpace(cell.Value?.ToString())
                            || index != cell.Column.Index))
                        {
                            throw new Exception(
                                $"{worksheet.Name} : One or more header(s) missing");
                            //break;
                        }

                        if (!string.IsNullOrWhiteSpace(cell.Value?.ToString())
                            && !headerRow.ContainsKey(cell.Column.Index))
                        {
                            if (!(columnsToSkip != null && columnsToSkip.Any(s => s.ToLower().Trim()
                                    == cell.StringValue.ToLower().Trim())))
                                headerRow.Add(cell.Column.Index, cell.StringValue);
                        }

                        index++;
                    }
                }

                //if (headerRow.Count() >= itemPropertyCount)
                //{
                //    headerRowIndex = i;
                //    break;
                //}
            }

            return (headerRowIndex, headerRow);
        }

        private static void SetPropertyValue<T>(PropertyInfo prop, T dataParsedItem, ExcelCell cell)
            where T : new()
        {
            try
            {
                switch (prop.PropertyType.Name)
                {
                    case "String":
                        prop.SetValue(dataParsedItem, cell.StringValue, null);
                        break;
                    case "Int32":
                        prop.SetValue(dataParsedItem, cell.IntValue, null);
                        break;
                    case "Int16":
                        prop.SetValue(dataParsedItem, cell.IntValue, null);
                        break;
                    case "Double":
                        prop.SetValue(dataParsedItem, cell.DoubleValue, null);
                        break;
                    case "boolean":
                        prop.SetValue(dataParsedItem, cell.BoolValue, null);
                        break;
                    case "DateTime":
                        prop.SetValue(dataParsedItem, cell.DateTimeValue, null);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                // skip conversion failures
            }
        }
    }
}
