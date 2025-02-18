using System.Text.Json.Serialization;

namespace CG.Web.MegaApiClient.Serialization
{
  using System;

  internal class CreateNodeRequest : RequestBase
  {
    private CreateNodeRequest(INode parentNode, NodeType type, string attributes, string encryptedKey, byte[] key, string completionHandle)
      : base("p")
    {
      ParentId = parentNode.Id;
      Nodes = new[]
      {
        new CreateNodeRequestData
        {
          Attributes = attributes,
          Key = encryptedKey,
          Type = type,
          CompletionHandle = completionHandle
        }
      };

      if (!(parentNode is INodeCrypto parentNodeCrypto))
      {
        throw new ArgumentException("parentNode node must implement INodeCrypto");
      }

      if (parentNodeCrypto.SharedKey != null)
      {
        Share = new ShareData(parentNode.Id);
        Share.AddItem(completionHandle, key, parentNodeCrypto.SharedKey);
      }
    }

    [JsonPropertyName("t")]
    public string ParentId { get; private set; }

    [JsonPropertyName("cr")]
    public ShareData Share { get; private set; }

    [JsonPropertyName("n")]
    public CreateNodeRequestData[] Nodes { get; private set; }

    public static CreateNodeRequest CreateFileNodeRequest(INode parentNode, string attributes, string encryptedkey, byte[] fileKey, string completionHandle)
    {
      return new CreateNodeRequest(parentNode, NodeType.File, attributes, encryptedkey, fileKey, completionHandle);
    }

    public static CreateNodeRequest CreateFolderNodeRequest(INode parentNode, string attributes, string encryptedkey, byte[] key)
    {
      return new CreateNodeRequest(parentNode, NodeType.Directory, attributes, encryptedkey, key, "xxxxxxxx");
    }

    internal class CreateNodeRequestData
    {
      [JsonPropertyName("h")]
      public string CompletionHandle { get; set; }

      [JsonPropertyName("t")]
      public NodeType Type { get; set; }

      [JsonPropertyName("a")]
      public string Attributes { get; set; }

      [JsonPropertyName("k")]
      public string Key { get; set; }
    }
  }
}
