using Core.Entity;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using SharedKernal;
using SharedKernal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_processing_fn_app.Activity
{
    public class InsertStaging
    {
            private readonly IRepository<StagingTable> repository;
            
        public InsertStaging(IRepository<StagingTable> repository)
        {
            this.repository = repository;
        }

        [FunctionName("Insert_Staging")]
        public async Task<bool> InsertStagingActivity([ActivityTrigger] List<ParsedYearDatacs> stagingData, ILogger log)
        {
            List<StagingTable> data = new List<StagingTable>();
            foreach(ParsedYearDatacs parsedYearDatacs in stagingData)
            {
                foreach (var yearData in parsedYearDatacs.YearData)
                {
                    data.Add(new StagingTable { Company = parsedYearDatacs.Company, Year = yearData.Key, Value = yearData.Value });
                }
            }
            await this.repository.AddRangeAsync(data);
            await this.repository.SaveChangesAsync();
            return true;
        }
    }
}
