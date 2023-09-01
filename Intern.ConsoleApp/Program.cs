using DataAccessLayer.Contect;
using EntityLayer.Concrete;
using Intern.WebAPI.Consumer;
using MassTransit;
using Polly;
using Context = DataAccessLayer.Contect.Context;

Random randomtemp = new Random();

List<int> randomNumbers = new List<int>();
var bus = Bus.Factory.CreateUsingRabbitMq(config =>
{

    config.Host("192.168.1.159", "/", h =>
    {
        h.Username("altis");
        h.Password("altis");

    });
    config.ReceiveEndpoint("Consumer.Random", e =>
    {

        //e.Consumer<ConsumerRMQ>();
    });
});

//Kanal başlatılıyor
await bus.StartAsync();
while (true)
{
    Console.WriteLine("Entera bas");
    Console.ReadLine();



    for (int i = 0; i < 5; i++)
    {
        int sayi = randomtemp.Next(300, 400);

        using (var _context = new Context())
        {
            var rand = new RandomTemperature
            {
                Tempreture = sayi,
            };
            bus.Publish(rand);//mesajın publish edilmesi 
            Console.WriteLine(sayi.ToString());
        }

    }
}
bus.Stop();