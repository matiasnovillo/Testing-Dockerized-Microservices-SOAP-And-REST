using FiyiStack.APIWithREST.EmailService;
using MassTransit;

namespace FiyiStack.APIWithREST.Consumers
{
    public class MovieConsultedConsumer(IEmailService emailService) : IConsumer<MovieConsulted>
    {
        private readonly IEmailService _emailService = emailService;

        public async Task Consume(ConsumeContext<MovieConsulted> context)
        {
            MovieConsulted MovieConsulted = context.Message;

            await _emailService.SendEmailAsync("novillo.matias1@gmail.com", $"Movie consulted: {MovieConsulted.Title}");
        }
    }
}
