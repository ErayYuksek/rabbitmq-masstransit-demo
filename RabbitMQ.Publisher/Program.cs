using RabbitMQ.Client;
using System.Text;

// Bağlantı oluşturma
var factory = new ConnectionFactory()
{
    Uri = new Uri("amqp://guest:guest@localhost:5672/")
    // CloudAMQP ise burayı değiştir:
    // Uri = new Uri("amqps://KULLANICI:ŞİFRE@HOST/VHOST")
};

// Bağlantıyı aç
using var connection = factory.CreateConnection();

// Kanal aç
using var channel = connection.CreateModel();

// Queue oluştur
channel.QueueDeclare(
    queue: "example-queue",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

// Mesajı byte[] haline çevir
byte[] message = Encoding.UTF8.GetBytes("Merhaba");

// Mesaj gönder
channel.BasicPublish(
    exchange: "",
    routingKey: "example-queue",
    basicProperties: null,
    body: message
);

Console.WriteLine("Mesaj gönderildi");
Console.ReadLine();