using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace data_processing_fn_app.Activity
{
    public class DownloadFile
    {
        private readonly IConfiguration _configuration;
        public DownloadFile(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        [FunctionName("Download_File")]
        public async Task<byte[]> DownloadFileActivity([ActivityTrigger] string blobpath, ILogger log)
        {
            BlobClient blob = new BlobClient(new Uri(blobpath), new AzureSasCredential(_configuration["DataUploadBlob:SASToken"]));
            var data  =  await blob.DownloadContentAsync();
            return data.Value.Content.ToArray();
        }

    }
}