namespace Entities.Models
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public MailRequest(string mail, string subject, string body)
        {
            ToEmail = mail;
            Subject = subject;
            Body = body;
        }
    }
}
