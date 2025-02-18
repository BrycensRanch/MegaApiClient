
namespace CG.Web.MegaApiClient.Serialization
{
  using System.Collections.Generic;
  using System.Text.Json.Serialization;

  internal abstract class RequestBase
  {
    protected RequestBase(string action)
    {
      Action = action;
      QueryArguments = new Dictionary<string, string>();
    }

    [JsonPropertyName("a")]
    public string Action { get; private set; }

    [JsonIgnore]
    public Dictionary<string, string> QueryArguments { get; }
  }
}
