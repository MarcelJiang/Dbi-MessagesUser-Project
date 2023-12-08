namespace Spg.Codechatter.API
{
    public class Settings
    {
        public MongoDBSettings MongoDB { get; set; }
        public string PublicURL { get; set; }
    }

    public class MongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}