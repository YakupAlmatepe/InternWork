using DataAccessLayer.Contect;
using EntityLayer.Concrete;
using Intern.WebAPI.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Intern.WebAPI.Consumer
{
    public class ConsumerRMQ : IConsumer<RandomTemperature>
    {
        private readonly MyHub _myHub;

      private  readonly Context _context;

       

        public ConsumerRMQ(MyHub myHub, Context context)
        {
            _myHub = myHub;
            _context = context;
        }

        public async Task Consume(ConsumeContext<RandomTemperature> context)
        {
            if (context.Message == null)
            {
                return;
            }

            _context.RandomTemperatures.Add(context.Message);
            _context.SaveChanges();

            _myHub.SendMessage("a");

            Console.WriteLine($"{context.Message}");
        }
    }
}





