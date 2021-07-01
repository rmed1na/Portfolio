using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PortfolioAPI.Models
{
    public class User : ModelMeta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string GetFullName()
        {
            var sb = new StringBuilder();

            sb.Append(FirstName);
            sb.Append(' ');
            sb.Append(LastName);

            return sb.ToString();
        }
    }
}
