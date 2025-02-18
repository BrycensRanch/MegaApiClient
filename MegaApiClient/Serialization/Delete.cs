using System.Text.Json.Serialization;

namespace CG.Web.MegaApiClient.Serialization
{

  internal class DeleteRequest : RequestBase
  {
    public DeleteRequest(INode node)
      : base("d")
    {
      Node = node.Id;
    }

    [JsonPropertyName("n")]
    public string Node { get; private set; }
  }
}
