using Newtonsoft.Json.Linq;

namespace tripscribe.Api.Integration.Test.Models;

public class ValidationModel
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string TraceId { get; set; }
    public JObject Errors { get; set; }
    
}