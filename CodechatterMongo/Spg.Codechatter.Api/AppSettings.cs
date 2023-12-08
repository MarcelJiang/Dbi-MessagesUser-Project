namespace Spg.Codechatter.API
{
    public class Settings
    {
        public MongoDBSettings MongoDB { get; set; } = new MongoDBSettings();
        public string PublicURL { get; set; } = string.Empty;
    }

    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
    }
}