using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;


namespace SubscriptionWebJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
      

        [Singleton]
        public static void ProcessQueueMessage([TimerTrigger("00:03:00")] TimerInfo timerInfo, TextWriter log)
        {
            var subscription = new SubscriptionController();
            subscription.GetSubscriptions();
        }
    }
}
