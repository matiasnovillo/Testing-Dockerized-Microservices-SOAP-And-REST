using FiyiStack.APIWithREST.Areas.Movie.DTOs;
using FiyiStack.APIWithREST.EmailService;
using MassTransit;

namespace FiyiStack.APIWithREST.Areas.Movie.Consumers
{
    public class MovieConsultedConsumer(IEmailService emailService) : IConsumer<MovieConsultedDTO>
    {
        private readonly IEmailService _emailService = emailService;

        public async Task Consume(ConsumeContext<MovieConsultedDTO> context)
        {
            MovieConsultedDTO MovieConsultedDTO = context.Message;

            await _emailService.SendEmailAsync("novillo.matias1@gmail.com", $"Movie consulted: {MovieConsultedDTO.Title}");
        }
    }
}
