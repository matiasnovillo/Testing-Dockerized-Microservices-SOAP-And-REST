namespace FiyiStack.APIWithREST.EmailService
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string content)
        {
            //Aquí iría la lógica para enviar un correo electrónico utilizando un servicio de correo electrónico, como SMTP, SendGrid, etc.
            return Task.CompletedTask;
        }
    }
}
