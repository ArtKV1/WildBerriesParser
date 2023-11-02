namespace WildBerries2
{
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class GetterOffersAndSellersIdsFromJson
    {
        [JsonProperty("state")]
        public long State { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("products")]
        public List<Product> Products { get; set; }
    }

    public partial class Product
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("supplierId")]
        public long SupplierId { get; set; }
    }

    public partial class GetterOffersAndSellersIdsFromJson
    {
        public static GetterOffersAndSellersIdsFromJson FromJson(string json) => JsonConvert.DeserializeObject<GetterOffersAndSellersIdsFromJson>(json, WildBerries2.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this GetterOffersAndSellersIdsFromJson self) => JsonConvert.SerializeObject(self, WildBerries2.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
