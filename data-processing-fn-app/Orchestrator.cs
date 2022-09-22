using Core.Entity;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using SharedKernal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_processing_fn_app
{
    public class Orchestrator
    {
        [FunctionName("OrchestratorFn")]
        public async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            // Replace "hello" with the name of your Durable Activity Function.
            var fileresponsedata = await context.CallActivityAsync<byte[]>("Download_File", context.GetInput<ProcessStartMessage>().BlobUrl);
            List<ParsedYearDatacs> stagingData = await context.CallActivityAsync<List<ParsedYearDatacs>>("Extract_Rows", (fileresponsedata, "sdfsdf"));
            bool stagingInsertionStatus = await context.CallActivityAsync<bool>("Insert_Staging", stagingData);

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }
    }
}
