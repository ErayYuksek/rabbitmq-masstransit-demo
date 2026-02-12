using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Bağlantı oluşturma
var factory = new ConnectionFactory()
{
    Uri = new Uri("amqp://guest:guest@localhost:5672/")
    // Local ise:
    // Uri = new Uri("amqp://guest:guest@localhost:5672/")
};

// Bağlantı aç
using var connection = factory.CreateConnection();

// Kanal aç
using var channel = connection.CreateModel();

// Queue oluştur (Publisher ile birebir aynı olmalı!)
channel.QueueDeclare(
    queue: "example-queue",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

Console.WriteLine("Mesaj bekleniyor...");

// Consumer oluştur
var consumer = new EventingBasicConsumer(channel);

// Kuyruktan mesaj dinleme
channel.BasicConsume(
    queue: "example-queue",
    autoAck: true,
    consumer: consumer
);

// Mesaj geldiğinde çalışacak event
consumer.Received += (model, e) =>
{
    var body = e.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Gelen mesaj: {message}");
};

Console.ReadLine();