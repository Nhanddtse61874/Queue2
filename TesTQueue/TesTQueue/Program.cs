using Azure.Storage.Queues;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;

namespace TesTQueue
{

    class Program
    {
        private static readonly string connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";

        static void Main(string[] args)
        {
            while(true)
            {
                Thread.Sleep(5000);

                CreateQueue(JsonSerializer.Serialize(new ModelA
                {
                    UserIdentity = "12345678ABC",
                    ProductsCode = new List<string> { "A101", "B202", "A103", "B204", "A105", "B206" },
                    RecommendedProduct = new List<List<string>>
                {
                    new List<string> { "A101", "B202", "C321" },
                    new List<string> { "A101", "B208", "C221" },
                    new List<string> { "A101", "B205", "C321" },
                    
                }
                }));
                Console.WriteLine("Write success");

                
            }


            
        }
        
        public static void CreateQueue(string message)
        {
            QueueClient queueClient = new QueueClient(connectionString, "azure");
            queueClient.SendMessage(Base64Encode(message));
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }

    public class ModelA
    {
        public string UserIdentity { get; set; }

        public List<string> ProductsCode { get; set; }

        public List<List<string>> RecommendedProduct { get; set; }
    }
}
