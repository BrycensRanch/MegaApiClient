using System.Text.Json.Serialization;

namespace CG.Web.MegaApiClient.Serialization
{

  internal class PreLoginRequest : RequestBase
  {
    public PreLoginRequest(string userHandle)
      : base("us0")
    {
      UserHandle = userHandle;
    }

    [JsonPropertyName("user")]
    public string UserHandle { get; private set; }
  }

  internal class PreLoginResponse
  {
    [JsonPropertyName("s")]
    public string Salt { get; private set; }

    [JsonPropertyName("v")]
    public int Version { get; private set; }
  }
}
