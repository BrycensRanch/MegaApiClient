
namespace CG.Web.MegaApiClient.Serialization
{
  using System.Text.Json.Serialization;

  internal class UploadUrlRequest : RequestBase
  {
    public UploadUrlRequest(long fileSize)
      : base("u")
    {
      Size = fileSize;
    }

    [JsonPropertyName("s")]
    public long Size { get; private set; }
  }

  internal class UploadUrlResponse
  {
    [JsonPropertyName("p")]
    public string Url { get; private set; }
  }
}
