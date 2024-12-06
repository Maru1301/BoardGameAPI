namespace BoardGame;

public class Config : IConfig
{
    public string AppName { get; set; } = string.Empty;
    public string DomainName { get; set; } = string.Empty;
    public ApiUrl ApiUrl { get; set; } = new();
    public EmailConfig EmailConfig { get; set; } = new();
    public MongoDBConfig MongoDBConfig { get; set;} = new();
    public RedisConfig RedisConfig { get; set;} = new();
    public JwtConfig JwtConfig { get; set;} = new();
}
public interface IConfig
{
    public string AppName { get; set; }
    public string DomainName { get; set; }
    public ApiUrl ApiUrl { get; set; }
    public EmailConfig EmailConfig { get; set; }
    public MongoDBConfig MongoDBConfig { get; set; }
    public RedisConfig RedisConfig { get; set; }
    public JwtConfig JwtConfig { get; set; }
}

public class ApiUrl
{
    public string ValidateEmail { get; set; } = string.Empty;
}

public class EmailConfig
{
    public string? Account { get; set; }
    public string? Password { get; set; }
}

public class MongoDBConfig
{
    public string? AtlasURI { get; set; }
    public string? DatabaseName { get; set; }
}

public class RedisConfig
{
    public string? ConnectionString { get; set; }
}

public class JwtConfig
{
    public bool ValidateIssuer { get; set; }
    public string? ValidIssuer { get; set; }
    public bool ValidateAudience { get; set; }
    public string? ValidAudience { get; set; }
    public bool ValidateLifetime { get; set; }
    public string? ExpiredTime { get; set; }
    public string? Algorithm { get; set; }
    public bool ValidateIssuerSigningKey { get; set; }
    public string? IssuerSigningKey { get; set; }
    public bool ExpirationEnabled { get; set; }
}
