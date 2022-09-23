using Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using SharedKernal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_processing_fn_app.Activity
{
    public class InsertFinal
    {
        private readonly IRepository<FinalData> repository;
        private readonly AppDbContext appDbContext;
        public InsertFinal(IRepository<FinalData> repository, AppDbContext appDbContext)
        {
            this.repository = repository;
            this.appDbContext = appDbContext;
        }

        [FunctionName("Insert_Final")]
        public async Task<bool> InsertFinalData([ActivityTrigger] ILogger log)
        {
            var command = "[dbo].[SP_Final_Insert]";
            appDbContext.Database.SetCommandTimeout(1800);//need more time than usual to execute calculation procedures.
            appDbContext.Set<FinalData>().FromSqlRaw($"EXEC {command}");
            return true;
        }
    }
}
