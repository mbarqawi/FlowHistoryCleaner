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
            Console.WriteLine($"will use the connection string {conn_str} ");
            if (!PromptConfirmation("Are you sure you want to clean all  Tables and Queues ?"))
            {
                return;
            }

            var TserviceClient = new TableServiceClient(conn_str);

            Pageable<TableItem> queryTableResults = TserviceClient.Query();
            foreach (TableItem table in queryTableResults)
            {
                if (table.Name.StartsWith("flow"))
                {
                    TserviceClient.DeleteTable(table.Name);
                    Console.WriteLine($"-Table {table.Name} is Deleted ");
                }

            }


            QueueServiceClient QserviceClient = new QueueServiceClient(conn_str);

            //list all queues in the storage account
            var myqueues = QserviceClient.GetQueues().AsPages();

            //then you can write code to list all the queue names          
            foreach (Azure.Page<QueueItem> queuePage in myqueues)
            {
                foreach (QueueItem queue in queuePage.Values)
                {
                    if (queue.Name.StartsWith("flow"))
                    {
                        QserviceClient.DeleteQueue(queue.Name);
                        Console.WriteLine($"-Queue {queue.Name} is Deleted ");
                    }


                }

            }

            Console.WriteLine("Done");
        }

        static private bool PromptConfirmation(string confirmText)
        {
            Console.Write(confirmText + " [y/n] : ");
            ConsoleKey response = Console.ReadKey(false).Key;
            Console.WriteLine();
            return (response == ConsoleKey.Y);
        }

    }
}
