using System;
using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace FlowHistoryCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string conn_str = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            Console.WriteLine($"will use the conniction string {conn_str} ");
            Console.ReadLine();
            var TserviceClient = new TableServiceClient(conn_str);
         
            Pageable<TableItem> queryTableResults = TserviceClient.Query();
            foreach (TableItem table in queryTableResults)
            {
                TserviceClient.DeleteTable(table.Name);
                Console.WriteLine($"-Table {table.Name}");
            }

            
            QueueServiceClient QserviceClient = new QueueServiceClient(conn_str);

            //list all queues in the storage account
            var myqueues = QserviceClient.GetQueues().AsPages();

            //then you can write code to list all the queue names          
            foreach (Azure.Page<QueueItem> queuePage in myqueues)
            {
                foreach (QueueItem q in queuePage.Values)
                {
                    QserviceClient.DeleteQueue(q.Name);
                    Console.WriteLine($"-Queue {q.Name}" );

                }

            }

            Console.WriteLine("Done");
        }
    }
}
