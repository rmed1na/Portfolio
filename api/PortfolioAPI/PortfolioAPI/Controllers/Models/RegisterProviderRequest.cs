using PortfolioAPI.Models.Enums;

namespace PortfolioAPI.Controllers.Models
{
    public class RegisterProviderRequest
    {
        public MailProviderCode ProviderCode { get; set; }
        public string HostServer { get; set; }
        public int Port { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
    }
}
