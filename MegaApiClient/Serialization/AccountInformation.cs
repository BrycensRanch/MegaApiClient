using System.Text.Json.Serialization;

namespace CG.Web.MegaApiClient.Serialization
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Runtime.Serialization;

  internal class AccountInformationRequest : RequestBase
  {
    public AccountInformationRequest()
      : base("uq")
    {
    }

    [JsonPropertyName("strg")]
    public int Storage => 1;

    [JsonPropertyName("xfer")]
    public int Transfer => 0;

    [JsonPropertyName("pro")]
    public int AccountType => 0;
  }

  internal class AccountInformationResponse : IAccountInformation
  {
    [JsonPropertyName("mstrg")]
    public long TotalQuota { get; private set; }

    [JsonPropertyName("cstrg")]
    public long UsedQuota { get; private set; }

    [JsonPropertyName("cstrgn")]
    private Dictionary<string, long[]> MetricsSerialized { get; set; }

    public IEnumerable<IStorageMetrics> Metrics { get; private set; }

    [OnDeserialized]
    public void OnDeserialized(StreamingContext context)
    {
      Metrics = MetricsSerialized.Select(x => (IStorageMetrics)new StorageMetrics(x.Key, x.Value));
    }

    private class StorageMetrics : IStorageMetrics
    {
      public StorageMetrics(string nodeId, long[] metrics)
      {
        NodeId = nodeId;
        BytesUsed = metrics[0];
        FilesCount = metrics[1];
        FoldersCount = metrics[2];
      }

      public string NodeId { get; }

      public long BytesUsed { get; }

      public long FilesCount { get; }

      public long FoldersCount { get; }
    }
  }
}
