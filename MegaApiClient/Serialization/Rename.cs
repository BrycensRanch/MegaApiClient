
namespace CG.Web.MegaApiClient.Serialization
{
  using System.Text.Json.Serialization;

  internal class RenameRequest : RequestBase
  {
    public RenameRequest(INode node, string attributes)
      : base("a")
    {
      Id = node.Id;
      SerializedAttributes = attributes;
    }

    [JsonPropertyName("n")]
    public string Id { get; private set; }

    [JsonPropertyName("attr")]
    public string SerializedAttributes { get; set; }
  }
}
