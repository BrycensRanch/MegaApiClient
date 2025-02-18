using System.Text.Json.Serialization;

namespace CG.Web.MegaApiClient.Serialization
{

  internal class DownloadUrlRequest : RequestBase
  {
    public DownloadUrlRequest(INode node)
      : base("g")
    {
      Id = node.Id;

      if (node is PublicNode publicNode)
      {
        QueryArguments["n"] = publicNode.ShareId;
      }
    }

    [JsonPropertyName("g")]
    public int G => 1;

    [JsonPropertyName("n")]
    public string Id { get; private set; }
  }

  internal class DownloadUrlRequestFromId : RequestBase
  {
    public DownloadUrlRequestFromId(string id)
      : base("g")
    {
      Id = id;
    }

    [JsonPropertyName("g")]
    public int G => 1;

    [JsonPropertyName("p")]
    public string Id { get; private set; }
  }

  internal class DownloadUrlResponse
  {
    [JsonPropertyName("g")]
    public string Url { get; private set; }

    [JsonPropertyName("s")]
    public long Size { get; private set; }

    [JsonPropertyName("at")]
    public string SerializedAttributes { get; set; }

    [JsonPropertyName("fa")]
    public string SerializedFileAttributes { get; set; }
  }
}
