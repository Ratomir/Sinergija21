using Azure.Storage.Queues;
using System.Text.Json;
using System.Text.Json.Serialization;

int numberOfMessages = Convert.ToInt32(Environment.GetCommandLineArgs()[1]);
string connectionString = Environment.GetEnvironmentVariable("QueueSinergija21");
string queueName = Environment.GetEnvironmentVariable("Queue");

Console.WriteLine($"Creating queue: {queueName}");

QueueClient queueClient = new QueueClient(connectionString, queueName, new QueueClientOptions() { MessageEncoding = QueueMessageEncoding.Base64 });

queueClient.CreateIfNotExists();

if (queueClient.Exists())
{
    var vote = new Vote();
    // Send a message to the queue
    for (int i = 1; i <= numberOfMessages; i++)
    {
        vote.SetProperties();
        Console.WriteLine($"Sending message - => {i}");
        queueClient.SendMessage(JsonSerializer.Serialize(vote));
    }
}

Console.WriteLine("See your containers!");
Console.ReadKey(true);

public class Vote 
{
    [JsonPropertyName("party")]
    public string Party { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("electoralPlace")]
    public string ElectoralPlace { get; set; }

    [JsonPropertyName("electoralUnit")]
    public string ElectoralUnit { get; set; }

    public void SetProperties()
    {
        Random random = new Random();
        Party = StaticValue.Parties[random.Next(0, 4)];
        Count = random.Next(1, 26);
        ElectoralPlace = StaticValue.Places[random.Next(0, 4)];
        ElectoralUnit = StaticValue.Regions[random.Next(0, 4)];
    }
}

public class StaticValue
{
    public static string[] Parties = new string[] { "it".ToUpper(), "csharp".ToUpper(), "java".ToUpper(), "python".ToUpper() };
    public static string[] Regions = new string[] { "A".ToUpper(), "B".ToUpper(), "C".ToUpper(), "D".ToUpper() };
    public static string[] Places = new string[] { "Sarajevo".ToUpper(), "Belgrade".ToUpper(), "Zagreb".ToUpper(), "Microsoft".ToUpper() };
}
