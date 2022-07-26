# Flow History Cleaner

Application that deletes the Tables and the Queues in the Logic app Standard storage account.

## How to download  the application

Open the Kudo https://docs.microsoft.com/en-us/azure/app-service/resources-kudu from Logic app site 

Then chose CMD 
![image](https://user-images.githubusercontent.com/891607/180943465-1c8ae261-91ce-4b75-ac41-242d4284fcbf.png)


Execute the below command to download the archive file 

```curl "https://github.com/mbarqawi/FlowHistoryCleaner/releases/download/main/Archive.tar" -OL```

Then create Directory by executing the below command

```md FlowHistoryCleaner ```

Then we will extract the file into the folder 

```tar -xvf Archive.tar -C FlowHistoryCleaner```

## How to Run it 
Make sure that the site is stop
Run the command ```FlowHistoryCleaner.exe``` in the CMD to start the cleaning process 

