namespace CG.Web.MegaApiClient.Serialization
{
    using System;
    using System.Collections.ObjectModel;
    using System.Net;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    internal class SessionHistoryRequest : RequestBase
    {
        public SessionHistoryRequest()
            : base("usl")
        {
        }

        [JsonPropertyName("x")]
        public int LoadSessionIds => 1;
    }

    [JsonConverter(typeof(SessionHistoryConverter))]
    internal class SessionHistoryResponse : Collection<ISession>
    {
        internal class SessionHistoryConverter : JsonConverter<SessionHistoryResponse>
        {
            public override SessionHistoryResponse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Null)
                {
                    return null;
                }

                var response = new SessionHistoryResponse();

                using (var doc = JsonDocument.ParseValue(ref reader))
                {
                    var root = doc.RootElement;
                    if (root.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var sessionArray in root.EnumerateArray())
                        {
                            if (sessionArray.ValueKind == JsonValueKind.Array)
                            {
                                response.Add(new Session(sessionArray));
                            }
                        }
                    }
                }

                return response;
            }

            public override void Write(Utf8JsonWriter writer, SessionHistoryResponse value, JsonSerializerOptions options)
            {
                throw new NotSupportedException();
            }

            private class Session : ISession
            {
                public Session(JsonElement jArray)
                {
                    try
                    {
                        LoginTime = jArray[0].GetInt64().ToDateTime();
                        LastSeenTime = jArray[1].GetInt64().ToDateTime();
                        Client = jArray[2].GetString();
                        IpAddress = IPAddress.Parse(jArray[3].GetString());
                        Country = jArray[4].GetString();
                        SessionId = jArray[6].GetString();
                        var isActive = jArray[7].GetInt64() == 1;

                        if (jArray[5].GetInt64() == 1)
                        {
                            Status |= SessionStatus.Current;
                        }

                        if (jArray[7].GetInt64() == 1)
                        {
                            Status |= SessionStatus.Active;
                        }

                        if (Status == SessionStatus.Undefined)
                        {
                            Status = SessionStatus.Expired;
                        }
                    }
                    catch (Exception ex)
                    {
                        Client = "Deserialization error: " + ex.Message;
                    }
                }

                public string Client { get; private set; }

                public IPAddress IpAddress { get; private set; }

                public string Country { get; private set; }

                public DateTime LoginTime { get; private set; }

                public DateTime LastSeenTime { get; private set; }

                public SessionStatus Status { get; private set; }

                public string SessionId { get; private set; }
            }
        }
    }
}
