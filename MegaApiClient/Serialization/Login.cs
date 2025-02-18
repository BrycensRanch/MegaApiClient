
namespace CG.Web.MegaApiClient.Serialization
{
  using System.Text.Json.Serialization;

  internal class LoginRequest : RequestBase
  {
    public LoginRequest(string userHandle, string passwordHash)
      : base("us")
    {
      UserHandle = userHandle;
      PasswordHash = passwordHash;
    }

    public LoginRequest(string userHandle, string passwordHash, string mfaKey)
      : base("us")
    {
      UserHandle = userHandle;
      PasswordHash = passwordHash;
      MFAKey = mfaKey;
    }

    [JsonPropertyName("user")]
    public string UserHandle { get; private set; }

    [JsonPropertyName("uh")]
    public string PasswordHash { get; private set; }

    [JsonPropertyName("mfa")]
    public string MFAKey { get; private set; }
  }

  internal class LoginResponse
  {
    [JsonPropertyName("csid")]
    public string SessionId { get; private set; }

    [JsonPropertyName("tsid")]
    public string TemporarySessionId { get; private set; }

    [JsonPropertyName("privk")]
    public string PrivateKey { get; private set; }

    [JsonPropertyName("k")]
    public string MasterKey { get; private set; }
  }
}
