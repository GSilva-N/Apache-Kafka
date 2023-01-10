using Confluent.Kafka;


var config = new ProducerConfig { BootstrapServers = "localhost:9092", Acks=Acks.Leader };

using var producer = new ProducerBuilder<string, string>(config).Build();
try
{   

    for (int i = 0; i < 10; i++)
    {
        var key = $"id_{i}";


        var response = producer.ProduceAsync("first_topic",
            new Message<string, string> { Value = "Producer .NET", Key = key }).GetAwaiter().GetResult();

        Console.WriteLine(
            $"Topico: {response.Topic},\n" +
            $"Key: {response.Key},\n" +
            $"Partição: {response.Partition.Value},\n" +
            $"Offset: {response.Offset},\n" +
            $"Timestamp: {response.Timestamp.UtcDateTime} \n"
            );
    }
    producer.Flush();

}
catch (Exception)
{

    throw;
}
