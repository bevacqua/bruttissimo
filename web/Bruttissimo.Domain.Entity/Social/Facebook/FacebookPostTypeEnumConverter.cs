using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Bruttissimo.Domain.Entity
{
    public class FacebookPostTypeEnumConverter : StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch (ArgumentException)
            {
                return FacebookPostType.Unknown;
            }
        }
    }
}