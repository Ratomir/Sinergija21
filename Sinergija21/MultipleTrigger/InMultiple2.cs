using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace MultipleTrigger
{
    public class InMultiple2
    {
        [FunctionName("InMultiple2")]
        public void Run([QueueTrigger("in-multiple-2", Connection = "Queue2")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"Storage queue 2: {myQueueItem}");
        }
    }
}
