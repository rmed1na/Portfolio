using PortfolioAPI.Models;

namespace PortfolioAPI
{
    public class AppSettings : IAppSettings
    {
        public DataSource DataSource { get; set; }
        public Encryption Encryption { get; set; }
    }

    public class DataSource
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }

    public class Encryption
    {
        public string Key { get; set; }
        public string Iv { get; set; }
    }
}
