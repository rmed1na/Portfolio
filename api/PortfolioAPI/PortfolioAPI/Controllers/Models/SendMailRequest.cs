namespace PortfolioAPI.Controllers.Models
{
    public class SendMailRequest
    {
        public string Message { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string SenderName { get; set; }
        public string Subject { get; set; }
    }
}
