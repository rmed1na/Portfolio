using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioAPI.Models
{
    public class UserApiKey : ModelMeta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserApiKeyId { get; set; }
        public int UserId { get; set; }
        public int ApiKeyId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; }
        
        [ForeignKey("ApiKeyId")]
        public ApiKey ApiKey { get; set; }
    }
}
