
namespace CG.Web.MegaApiClient.Serialization
{
  using System.Text.Json.Serialization;

  internal class MoveRequest : RequestBase
  {
    public MoveRequest(INode node, INode destinationParentNode)
      : base("m")
    {
      Id = node.Id;
      DestinationParentId = destinationParentNode.Id;
    }

    [JsonPropertyName("n")]
    public string Id { get; private set; }

    [JsonPropertyName("t")]
    public string DestinationParentId { get; private set; }
  }
}
