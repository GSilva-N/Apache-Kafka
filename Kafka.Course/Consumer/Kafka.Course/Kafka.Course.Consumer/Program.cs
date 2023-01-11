using Confluent.Kafka;

var config = new ConsumerConfig
{
    GroupId = "dotenet-consumer-group",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
    //PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky
};


using var consumer = new ConsumerBuilder<string, string>(config).Build();

consumer.Subscribe("first_topic");

CancellationTokenSource token = new();

try
{
    while (true)
    {
        var response = consumer.Consume(token.Token);

        if (response.Message != null)
        {
            Console.WriteLine(
            $"Topico: {response.Topic},\n" +
            $"Mensagem: {response.Message.Value},\n" +
            $"Key: {response.Message.Key},\n" +
            $"Partição: {response.Partition.Value},\n" +
            $"Offset: {response.Offset},\n" +
            $"Timestamp: {response.Timestamp.UtcDateTime} \n");
        }

        Console.WriteLine("...");
    }
}
catch (ConsumeException)
{
    throw;
}

