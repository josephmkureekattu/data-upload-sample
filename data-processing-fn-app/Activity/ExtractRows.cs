using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Entity;
using data_processing_fn_app.Extensions;
using GemBox.Spreadsheet;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SharedKernal.Models;

namespace data_processing_fn_app.Activity
{
    public class ExtractRows
    {


        [FunctionName("Extract_Rows")]
        public List<ParsedYearDatacs> ExtractRowsActivity([ActivityTrigger] (byte[] fileContent, string x) request, ILogger log)
        {
            var workBook = GetWorkbook(request.fileContent, new XlsxLoadOptions());
            var selectedWorksheet = workBook.Worksheets[0];
            var result = selectedWorksheet.Convert<ParsedYearDatacs>();
            return result;
        }

        public ExcelFile GetWorkbook(byte[] fileContent, LoadOptions options)
        {
            //TODO : Replace with actual key using secret service
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            var inputStream = new MemoryStream(fileContent);
            return ExcelFile.Load(inputStream, options);
        }

    }
}