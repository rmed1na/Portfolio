using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioAPI.Models
{
    public class ApiKey : ModelMeta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApiKeyId { get; set; }
        public string Key { get; set; }
    }
}
