using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace MultipleTrigger
{
    public class InMultiple1
    {
        [FunctionName("InMultiple1")]
        public void Run([QueueTrigger("in-multiple-1", Connection = "Queue1")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"Storage queue 1: {myQueueItem}");
        }
    }
}
