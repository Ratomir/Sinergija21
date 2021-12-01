using Azure.Storage.Queues; // Namespace for Queue storage types

int numberOfMessages = Convert.ToInt32(Environment.GetCommandLineArgs()[1]);

Task task1 = new Task(() => InsertMessages(1, numberOfMessages));
Task task2 = new Task(() => InsertMessages(2, numberOfMessages));

task1.Start();
task2.Start();

Task.WaitAll(task1, task2);

Console.WriteLine("See your containers!");
Console.ReadKey(true);


void InsertMessages(int taskId, int count)
{
    string connectionString = Environment.GetEnvironmentVariable("QueueConnectionString" + taskId);
    string queueName = Environment.GetEnvironmentVariable("Queue" + taskId);

    Console.WriteLine($"Creating queue: {queueName}");

    QueueClient queueClient = new QueueClient(connectionString, queueName, new QueueClientOptions() { MessageEncoding = QueueMessageEncoding.Base64 });
    // Create the queue if it doesn't already exist
    queueClient.CreateIfNotExists();

    if (queueClient.Exists())
    {
        // Send a message to the queue
        for (int i = 1; i <= numberOfMessages; i++)
        {
            Console.WriteLine($"Sending message - Queue {taskId} => " + i);
            queueClient.SendMessage($"Sinergija21_queue_{taskId}_message_" + i);
        }
    }
}

