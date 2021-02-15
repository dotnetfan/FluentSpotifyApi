using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Albums;
using FluentSpotifyApi.Model.Artists;
using FluentSpotifyApi.Model.Episodes;
using FluentSpotifyApi.Model.Playlists;
using FluentSpotifyApi.Model.Shows;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Serialization
{
    internal class EntityConverter : JsonConverter<EntityBase>
    {
        private static readonly string TypePropertyName;

        static EntityConverter()
        {
            TypePropertyName = typeof(EntityBase).GetProperty(nameof(EntityBase.Type)).GetCustomAttribute<JsonPropertyNameAttribute>().Name;
        }

        public override EntityBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                var entityTypeName = jsonDocument.RootElement.TryGetProperty(TypePropertyName, out var typeProperty) ? typeProperty.GetString() : null;
                var entityType = this.GetEntityType(entityTypeName);

                var jsonObject = jsonDocument.RootElement.GetRawText();
                var result = (EntityBase)JsonSerializer.Deserialize(jsonObject, entityType);

                return result;
            }
        }

        public override void Write(Utf8JsonWriter writer, EntityBase value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, options);
        }

        private Type GetEntityType(string type)
        {
            switch (type)
            {
                case "artist":
                    return typeof(Artist);
                case "album":
                    return typeof(Album);
                case "track":
                    return typeof(Track);
                case "show":
                    return typeof(Show);
                case "episode":
                    return typeof(Episode);
                case "playlist":
                    return typeof(Playlist);
                case "user":
                    return typeof(PublicUser);
                default:
                    return typeof(UnknownEntity);
            }
        }
    }
}
