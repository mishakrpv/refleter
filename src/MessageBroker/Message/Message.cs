using System.Text.Json.Serialization;

namespace MessageBroker.Message;

public record Message
{
    [JsonInclude]
    public Guid Id { get; set; }
    
    [JsonInclude]
    public DateTime CreationDate { get; set; }
}