using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Timers;


namespace SubscriptionWebJob
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    public class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var config = new JobHostConfiguration();
           
            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }
            config.UseTimers();
            var host = new JobHost(config);
            try
            {
                host.RunAndBlock();
            }
            catch(Exception ex)
            {
                var message = ex.Message;
            }
            // The following code ensures that the WebJob will be running continuously
           
        }
    }
}
