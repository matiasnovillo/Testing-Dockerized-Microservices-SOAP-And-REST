namespace FiyiStack.APIWithREST.EmailService
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string email, string content);
    }
}
