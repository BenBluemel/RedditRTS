using Newtonsoft.Json;

namespace RedditRTS.Infrastructure.Apis.Reddit.Models
{
    public class Post
    {
        [JsonProperty("subreddit_name_prefixed")]
        public string? SubredditNamePrefixed { get; set; }
        public string? Subreddit { get; set; }
        public string? Name { get; set; }
        [JsonProperty("author_fullname")]
        public string? AuthorFullname { get; set; }
        public string? Author { get; set; }
        [JsonProperty("created_utc")]
        public double? CreatedUtcDouble { get; set; }
        public DateTimeOffset? CreatedUtc =>
            CreatedUtcDouble != null ? DateTimeOffset.FromUnixTimeSeconds((long)CreatedUtcDouble) : null;
            
        public string? Title { get; set; }
        public string? SelfText { get; set; }
        public int? Ups { get; set; }
        public int? Downs { get; set; }
        public bool? Hidden { get; set; }
        [JsonProperty("subreddit_type")]
        public string? SubredditType { get; set; }
    }

    public class RedditUnixDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(double);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Float)
            {
                return (long)reader.Value;
            }
            else
            {
                throw new JsonSerializationException($"Unexpected TokenType {reader.TokenType} should be Float");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is long)
            {
                writer.WriteValue(value);
            }
            else
            {
                throw new JsonSerializationException($"Unexpected value {value} should be long");
            }
            
        }
    }
}