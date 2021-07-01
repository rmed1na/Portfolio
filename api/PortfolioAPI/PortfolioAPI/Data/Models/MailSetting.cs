using PortfolioAPI.Models;
using PortfolioAPI.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioAPI.Data.Models
{
    public class MailSetting : ModelMeta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MailSettingId { get; set; }
        public MailProviderCode ProviderCode { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Sender { get; set; }
        public byte[] Password { get; set; }
    }
}
