namespace CG.Web.MegaApiClient.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Cryptography;

    [JsonConverter(typeof(ShareDataConverter))]
    internal class ShareData
    {
        private readonly IList<ShareDataItem> _items;

        public ShareData(string nodeId)
        {
            NodeId = nodeId;
            _items = new List<ShareDataItem>();
        }

        public string NodeId { get; private set; }

        public IEnumerable<ShareDataItem> Items => _items;

        public void AddItem(string nodeId, byte[] data, byte[] key)
        {
            var item = new ShareDataItem
            {
                NodeId = nodeId,
                Data = data,
                Key = key
            };

            _items.Add(item);
        }

        public class ShareDataItem
        {
            public string NodeId { get; set; }

            public byte[] Data { get; set; }

            public byte[] Key { get; set; }
        }
    }

    internal class ShareDataConverter : JsonConverter<ShareData>
    {
        public override void Write(Utf8JsonWriter writer, ShareData value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                throw new ArgumentException("invalid data to serialize");
            }

            writer.WriteStartArray();

            writer.WriteStartArray();
            writer.WriteStringValue(value.NodeId);
            writer.WriteEndArray();

            writer.WriteStartArray();
            foreach (var item in value.Items)
            {
                writer.WriteStringValue(item.NodeId);
            }
            writer.WriteEndArray();

            writer.WriteStartArray();
            var counter = 0;
            foreach (var item in value.Items)
            {
                writer.WriteNumberValue(0);
                writer.WriteNumberValue(counter++);
                writer.WriteStringValue(Crypto.EncryptKey(item.Data, item.Key).ToBase64());
            }
            writer.WriteEndArray();

            writer.WriteEndArray();
        }

        public override ShareData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    [DebuggerDisplay("Id: {Id} / Key: {Key}")]
    internal class SharedKey
    {
        public SharedKey(string id, string key)
        {
            Id = id;
            Key = key;
        }

        [JsonPropertyName("h")]
        public string Id { get; private set; }

        [JsonPropertyName("k")]
        public string Key { get; private set; }
    }
}
