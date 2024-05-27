namespace DESAFIO.BLOG.API.Models
{
    public class LogLevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class Smtp
    {
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string LocalDomain { get; set; }
        public bool UsePickupFolder { get; set; }
        public string PickupFolder { get; set; }
        public string FromName { get; set; }
        public string FromAddress { get; set; }
    }

    public class HealthCheck
    {
        public string Name { get; set; }
        public string Uri { get; set; }
    }

    public class HealthChecksUI
    {
        public List<HealthCheck> HealthChecks { get; set; }
    }

    public class RabbitMqConnection
    {
        public bool AutomaticRecoveryEnabled { get; set; }
        public string EventBusExchange { get; set; }
        public string HostName { get; set; }
        public string Password { get; set; }
        public int RequestedHeartbeat { get; set; }
        public string Username { get; set; }
        public string VirtualHost { get; set; }
        public string Port { get; set; }
    }

    public class WorkerServiceBackground
    {
        public string Name { get; set; }
        public string Provider { get; set; }
        public string URI { get; set; }
        public string Key { get; set; }
        public int Interval { get; set; }
        public List<string> UriParameters { get; set; }
        public List<string> Fallback { get; set; } = new List<string>();
    }

    public class MongoDb
    {
        public string Connection { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
    }

    public class JWT
    {
        public string Token { get; set; }
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }

    public class WhatsApp
    {
        public string Type { get; set; }
        public string Provider { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Endpoint { get; set; }
    }

    public class Telegram
    {
        public string Type { get; set; }
        public string Provider { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public List<string> Groups { get; set; }
    }

    public class Channel
    {
        public WhatsApp WhatsApp { get; set; }
        public Telegram Telegram { get; set; }
        public Telegram TelegramNotifications { get; set; }
    }

    public class ExternalService
    {
        public string Name { get; set; }
        public string Provider { get; set; }
        public string Uri { get; set; }
        public List<string> Fallback { get; set; } = new List<string>();
        public string Key { get; set; }
    }

    public class AppSettings
    {
        public Logging Logging { get; set; }
        public Smtp Smtp { get; set; }
        public string TokenKey { get; set; }
        public HealthChecksUI HealthChecksUI { get; set; }
        public RabbitMqConnection RabbitMqConnection { get; set; }
        public List<WorkerServiceBackground> WorkerServiceBackground { get; set; }
        public MongoDb MongoDb { get; set; }
        public ConnectionString ConnectionStrings { get; set; }
        public JWT JWT { get; set; }
        public Dictionary<string, Channel> Channels { get; set; }
        public List<ExternalService> ExternalServices { get; set; }
    }

    public class ConnectionString
    {
        public string EidosFashionContext { get; set; }
    }
}
