namespace CG.Web.MegaApiClient.Serialization
{
  using System;
  using System.Collections.Generic;
  using System.Text.Json;
  using System.Text.Json.Serialization;

  internal class NodeConverter : JsonConverter<Node>
  {
    private readonly byte[] _masterKey;
    private List<SharedKey> _sharedKeys;

    public NodeConverter(byte[] masterKey, ref List<SharedKey> sharedKeys)
    {
      _masterKey = masterKey;
      _sharedKeys = sharedKeys;
    }

    public override Node Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      if (reader.TokenType == JsonTokenType.Null)
      {
        return null;
      }

      using var doc = JsonDocument.ParseValue(ref reader);
      var jsonElement = doc.RootElement;
      var target = new Node(_masterKey, ref _sharedKeys);

      foreach (var property in jsonElement.EnumerateObject())
      {
        if (property.Name == "sharedKeys")
        {
          _sharedKeys = JsonSerializer.Deserialize<List<SharedKey>>(property.Value.GetRawText(), options);
        }
        else
        {
          var propertyInfo = typeof(Node).GetProperty(property.Name);
          if (propertyInfo != null)
          {
            var value = JsonSerializer.Deserialize(property.Value.GetRawText(), propertyInfo.PropertyType, options);
            propertyInfo.SetValue(target, value);
          }
        }
      }

      return target;
    }

    public override void Write(Utf8JsonWriter writer, Node value, JsonSerializerOptions options)
    {
      throw new NotSupportedException();
    }
  }
}
