﻿using Newtonsoft.Json;
using UnityEngine;
using System;

namespace Sergei_Lind.LS.Runtime.Utilities
{
    public sealed class Vector2Converter : JsonConverter<Vector2>
    {
        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(value.x);
            writer.WritePropertyName("y");
            writer.WriteValue(value.y);
            writer.WriteEndObject();
        }

        public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            float x = 0, y = 0;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    var prop = (string)reader.Value;
                    reader.Read();
                    switch (prop)
                    {
                        case "x":
                            x = Convert.ToSingle(reader.Value);
                            break;
                        case "y":
                            y = Convert.ToSingle(reader.Value);
                            break;
                    }
                }
                else if (reader.TokenType == JsonToken.EndObject)
                {
                    break;
                }
            }

            return new Vector2(x, y);
        }
    }
}