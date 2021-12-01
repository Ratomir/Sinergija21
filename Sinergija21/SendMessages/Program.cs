using Azure.Storage.Queues; // Namespace for Queue storage types


int numberOfMessages = Convert.ToInt32(Environment.GetCommandLineArgs()[1]);
string connectionString = Environment.GetEnvironmentVariable("QueueSinergija21");
string queueName = Environment.GetEnvironmentVariable("Queue");

Console.WriteLine($"Creating queue: {queueName}");

QueueClient queueClient = new QueueClient(connectionString, queueName, new QueueClientOptions() { MessageEncoding = QueueMessageEncoding.Base64 });
// Create the queue if it doesn't already exist
queueClient.CreateIfNotExists();

if (queueClient.Exists())
{
    // Send a message to the queue
    for (int i = 1; i <= numberOfMessages; i++)
    {
        Console.WriteLine("Sending message => " + i);
        queueClient.SendMessage("Sinergija21_message_" + i);
    }
}

Console.WriteLine("See your containers!");
Console.ReadKey(true);
