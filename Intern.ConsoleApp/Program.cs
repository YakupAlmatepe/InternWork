using DataAccessLayer.Contect;
using EntityLayer.Concrete;
using MassTransit;

Random randomtemp = new Random();

List<int> randomNumbers = new List<int>();
var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    
    factory.Host("192.168.1.159", "/", h =>
    {
        h.Username("altis");
        h.Password("altis");

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
