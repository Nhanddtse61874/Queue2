using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Persistence.Models;

namespace TriggerQueue_2
{
    public static class Function1
    {
        
        [FunctionName("QueueTrigger2")]
        public static async Task Run(
            [QueueTrigger("azure", Connection = "AzureWebJobsStorage")]
            string message, ILogger log)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44309/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                
                HttpResponseMessage response = 
                    await client.PostAsJsonAsync("api/RecommenceByHobby", JsonSerializer.Deserialize<Recommence>(message));

                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    Uri returnUrl = response.Headers.Location;
                    Console.WriteLine(returnUrl);
                }
                ///
                HttpResponseMessage response2 =
                    await client.PostAsJsonAsync("api/RecommenceByPrice", JsonSerializer.Deserialize<Recommence>(message));

                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    Uri returnUrl = response.Headers.Location;
                    Console.WriteLine(returnUrl);
                }//abc
            }
        }
    }
}
