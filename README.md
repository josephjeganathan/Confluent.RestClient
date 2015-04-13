# Confluent.RestClient
[![Build status](https://ci.appveyor.com/api/projects/status/ddit8x0r5q7c6hx6?svg=true)](https://ci.appveyor.com/project/josephjeganathan/confluent-restclient) [![](http://img.shields.io/nuget/v/Confluent.RestClient.svg?style=flat-square)](http://www.nuget.org/packages/Confluent.RestClient/)  [![](http://img.shields.io/nuget/dt/Confluent.RestClient.svg?style=flat-square)](http://www.nuget.org/packages/Confluent.RestClient/)


---
Confluent.RestClient is a .NET client libaray for Confluent.io REST API

REST API documentation: http://confluent.io/docs/current/kafka-rest/docs/index.html

### Implemting your own Confluent client settings.

Confluent client settings can be implemented by extending `IConfluentClientSettings`, for example

```C#
public class MyConfluentClientSettings : IConfluentClientSettings
{
    public string KafkaBaseUrl
    {
        get
        {
            return ConfigurationManager.AppSettings["Confluent.KafkaBaseUrl"];
        }
    }
}
```

---
You can check out all the client operartions in the `Confluent.TestHarness` app. They are very easy to use, for example:

#### Publishing Avro data to `TestTopic`

```C#

// Contract to publish as Avro data 
[DataContract]
public class Person
{
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public int Age { get; set; }
}


IConfluentClient client = new ConfluentClient(settings);

var records = new[]
{
    new AvroRecord<string, Person>
    {
        PartitionId = Convert.ToInt32(0),
        Value = new Person { Name = Guid.NewGuid().ToString("N"), Age = 25 }
    },
    new AvroRecord<string, Person>
    {
        Value = new Person { Name = Guid.NewGuid().ToString("N"), Age = 26 }
    }
};
var recordSet = new AvroRecordSet<string, Person>(records)
{
    //Creating schema using "Microsoft.Hadoop.Avro" - https://www.nuget.org/packages/Microsoft.Hadoop.Avro/
    ValueSchema = AvroSerializer.Create<Person>().ReaderSchema.ToString()
};

await client.PublishAsAvroAsync("TestTopic", recordSet);
```

#### Creating consumer instance to consume Avro data

```C#
IConfluentClient client = new ConfluentClient(settings);
                
var request = new CreateConsumerRequest
{
    // Confluent API will create a new InstanceId if not supplied
    InstanceId = "TestConsumerInstance",
    MessageFormat = MessageFormat.Avro
};

ConfluentResponse<ConsumerInstance> response = await client.CreateConsumerAsync("TestConsumerGroup", request);
ConsumerInstance consumerInstance = response.Payload;
```

#### Consume Avro data

```C#
IConfluentClient client = new ConfluentClient(settings);

ConfluentResponse<List<AvroMessage<string, Person>>> response
    = await client.ConsumeAsAvroAsync<string, Person>(consumerInstance, "TestTopic");

foreach (AvroMessage<string, Person> message in response.Payload)
{
    Person person = message.Value;
    Console.WriteLine("Name: {0}, Age: {1}", person.Name, person.Age);
}
```

#### Commit offset

```C#
await client.CommitOffsetAsync(consumerInstance);
```
