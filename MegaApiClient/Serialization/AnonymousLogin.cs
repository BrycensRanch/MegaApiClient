using System.Text.Json.Serialization;

namespace CG.Web.MegaApiClient.Serialization
{
  internal class AnonymousLoginRequest : RequestBase
  {
    public AnonymousLoginRequest(string masterKey, string temporarySession)
      : base("up")
    {
      MasterKey = masterKey;
      TemporarySession = temporarySession;
    }

    [JsonPropertyName("k")]
    public string MasterKey { get; set; }

    [JsonPropertyName("ts")]
    public string TemporarySession { get; set; }
  }
}
