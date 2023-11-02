namespace WildBerries2
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class GetterTotalOffers
    {
        [JsonProperty("state")]
        public long State { get; set; }

        [JsonProperty("data")]
        public Data2 Data { get; set; }
    }

    public partial class Data2
    {
        [JsonProperty("filters")]
        public List<Filter> Filters { get; set; }

        [JsonProperty("previews")]
        public Dictionary<string, long> Previews { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public partial class Filter
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("maxselect")]
        public long Maxselect { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }
    }

    public partial class GetterTotalOffers
    {
        public static GetterTotalOffers FromJson(string json) => JsonConvert.DeserializeObject<GetterTotalOffers>(json, WildBerries2.Converter.Settings);
    }
}
