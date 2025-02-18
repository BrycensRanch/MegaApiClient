using System.Text.Json.Serialization;

namespace CG.Web.MegaApiClient.Serialization
{

  internal class GetDownloadLinkRequest : RequestBase
  {
    public GetDownloadLinkRequest(INode node)
      : base("l")
    {
      Id = node.Id;
    }

    [JsonPropertyName("n")]
    public string Id { get; private set; }
  }
}
