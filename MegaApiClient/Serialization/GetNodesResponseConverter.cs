namespace CG.Web.MegaApiClient.Serialization
{
  using System;
  using System.Text.Json;
  using System.Text.Json.Serialization;

  internal class GetNodesResponseConverter : JsonConverter<GetNodesResponse>
  {
    private readonly byte[] _masterKey;

    public GetNodesResponseConverter(byte[] masterKey)
    {
      _masterKey = masterKey;
    }

    public override GetNodesResponse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      if (reader.TokenType == JsonTokenType.Null)
      {
        return null;
      }

      using var doc = JsonDocument.ParseValue(ref reader);
      var jsonElement = doc.RootElement;
      var target = new GetNodesResponse(_masterKey);

      foreach (var property in jsonElement.EnumerateObject())
      {
        var propertyInfo = typeof(GetNodesResponse).GetProperty(property.Name);
        if (propertyInfo != null)
        {
          var value = JsonSerializer.Deserialize(property.Value.GetRawText(), propertyInfo.PropertyType, options);
          propertyInfo.SetValue(target, value);
        }
      }

      return target;
    }

    public override void Write(Utf8JsonWriter writer, GetNodesResponse value, JsonSerializerOptions options)
    {
      throw new NotSupportedException();
    }
  }
}
