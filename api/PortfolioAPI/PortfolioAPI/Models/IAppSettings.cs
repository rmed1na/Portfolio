namespace PortfolioAPI.Models
{
    public interface IAppSettings
    {
        DataSource DataSource { get; set; }
        Encryption Encryption { get; set; }
    }
}
