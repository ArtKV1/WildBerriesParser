namespace WildBerries2
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class GetterProductCardFromJson
    {
        [JsonProperty("imt_id")]
        public long ImtId { get; set; }

        [JsonProperty("nm_id")]
        public long NmId { get; set; }

        [JsonProperty("imt_name")]
        public string ImtName { get; set; }

        [JsonProperty("subj_name")]
        public string SubjName { get; set; }

        [JsonProperty("subj_root_name")]
        public string SubjRootName { get; set; }

        [JsonProperty("vendor_code")]
        public string VendorCode { get; set; }

        [JsonProperty("kinds")]
        public List<string> Kinds { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("grouped_options")]
        public List<GroupedOption> GroupedOptions { get; set; }

        [JsonProperty("options")]
        public List<Option> Options { get; set; }

        [JsonProperty("compositions")]
        public List<Composition> Compositions { get; set; }

        [JsonProperty("sizes_table")]
        public SizesTable SizesTable { get; set; }

        [JsonProperty("certificate")]
        public Certificate Certificate { get; set; }

        [JsonProperty("nm_colors_names")]
        public string NmColorsNames { get; set; }

        [JsonProperty("colors")]
        public List<long> Colors { get; set; }

        [JsonProperty("full_colors")]
        public List<FullColor> FullColors { get; set; }

        [JsonProperty("selling")]
        public Selling Selling { get; set; }

        [JsonProperty("media")]
        public Media Media { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Certificate
    {
    }

    public partial class Composition
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("subject_id")]
        public long SubjectId { get; set; }

        [JsonProperty("subject_root_id")]
        public long SubjectRootId { get; set; }

        [JsonProperty("chrt_ids")]
        public List<long> ChrtIds { get; set; }
    }

    public partial class FullColor
    {
        [JsonProperty("nm_id")]
        public long NmId { get; set; }
    }

    public partial class GroupedOption
    {
        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        [JsonProperty("options")]
        public List<Option> Options { get; set; }
    }

    public partial class Option
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class Media
    {
        [JsonProperty("has_video")]
        public bool HasVideo { get; set; }

        [JsonProperty("photo_count")]
        public long PhotoCount { get; set; }
    }

    public partial class Selling
    {
        [JsonProperty("brand_name")]
        public string BrandName { get; set; }

        [JsonProperty("supplier_id")]
        public long SupplierId { get; set; }
    }

    public partial class SizesTable
    {
        [JsonProperty("details_props")]
        public List<string> DetailsProps { get; set; }

        [JsonProperty("values")]
        public List<Value> Values { get; set; }
    }

    public partial class Value
    {
        [JsonProperty("tech_size")]
        public string TechSize { get; set; }

        [JsonProperty("chrt_id")]
        public long ChrtId { get; set; }

        [JsonProperty("details")]
        public List<string> Details { get; set; }

        [JsonProperty("skus")]
        public List<string> Skus { get; set; }
    }

    public partial class GetterProductCardFromJson
    {
        public static GetterProductCardFromJson FromJson(string json) => JsonConvert.DeserializeObject<GetterProductCardFromJson>(json, WildBerries2.Converter.Settings);
    }
}
