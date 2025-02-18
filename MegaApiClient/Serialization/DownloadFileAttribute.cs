using System.Text.Json.Serialization;

namespace CG.Web.MegaApiClient.Serialization
{

  internal class DownloadFileAttributeRequest : RequestBase
  {
    public DownloadFileAttributeRequest(string fileAttributeHandle)
      : base("ufa")
    {
      Id = fileAttributeHandle;
    }

    [JsonPropertyName("ssl")]
    public int Ssl => 2;

    [JsonPropertyName("r")]
    public int R => 1;

    [JsonPropertyName("fah")]
    public string Id { get; private set; }
  }

  internal class DownloadFileAttributeResponse
  {
    [JsonPropertyName("p")]
    public string Url { get; private set; }
  }
}
