# Flow History Cleaner

Application that deletes the Tables and the Queues in the Logic app Standard storage account.

## How to download  the application

Open the Kudo https://docs.microsoft.com/en-us/azure/app-service/resources-kudu from Logic app site 

Then chose CMD 

![image](https://user-images.githubusercontent.com/891607/180943465-1c8ae261-91ce-4b75-ac41-242d4284fcbf.png)


Execute the below command to clone the GitHub repository 

`git clone https://github.com/mbarqawi/FlowHistoryCleaner.git`


the above commend will create a folder 

`FlowHistoryCleaner`

Then you will have to publish the solution to be able to use the executable 



```
Cd FlowHistoryCleaner
dotnet publish
Cd FlowHistoryCleaner\bin\Debug\netcoreapp3.1\publish
```




You need to set the environment variable DOTNET_ADD_GLOBAL_TOOLS_TO_PATH to false to be able to run the dotnet publish  on the Kudu





# How to Run it
Make sure that the site is stop Run the command FlowHistoryCleaner.exe in the CMD to start the cleaning process

# How Code works 
The code is a .net core application it extracts the storage account connection string from the environment variables



string conn_str = Environment.GetEnvironmentVariable("AzureWebJobsStorage");


Then list all the tables and delete them 



``` var TserviceClient = new TableServiceClient(conn_str);      
 Pageable<TableItem> queryTableResults = TserviceClient.Query();
 foreach (TableItem table in queryTableResults)
     {
        TserviceClient.DeleteTable(table.Name);
        Console.WriteLine($"-Table {table.Name}");
      }
```

Then list all Queues and delete them 


```
QueueServiceClient QserviceClient = new QueueServiceClient(conn_str);

//list all queues in the storage account
var myqueues = QserviceClient.GetQueues().AsPages();
//then you can write code to list all the queue names          
 foreach (Azure.Page<QueueItem> queuePage in myqueues)
            {
                foreach (QueueItem q in queuePage.Values)
                {
                    QserviceClient.DeleteQueue(q.Name);
                    //Console.WriteLine($"-Queue {q.Name}" );

                }

            }

```




