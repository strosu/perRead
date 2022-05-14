using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace PerRead.Backend.Models.FrontEnd
{
    /// <summary>
    /// Represents an article "card", sumarizing the article. 
    /// WIll be used to mostly send the user to another resource (e.g. the full article, author, tag etc)
    /// </summary>
    public class FEArticlePreview
    {
        public string ArticleId { get; set; }

        public IEnumerable<FEAuthorPreview> AuthorPreviews { get; set; }

        public IEnumerable<FETagPreview> TagPreviews { get; set; }

        public string ArticleTitle { get; set; }

        public DateTime ArticleCreatedAt { get; set; }

        public string ArticlePreview { get; set; }

        public uint ArticlePrice { get; set; }

        public string ArticleImageUrl { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ReadingState ReadingState { get; set; }

        public IEnumerable<FESectionPreview> SectionPreviews { get; set; }
    }

    public enum ReadingState
    {
        [EnumMember(Value = nameof(Purchased))]
        Purchased,

        [EnumMember(Value = nameof(WithinBuyingLimit))]
        WithinBuyingLimit,

        [EnumMember(Value = nameof(OutsideOfLimitButAffordable))]
        OutsideOfLimitButAffordable,

        [EnumMember(Value = nameof(Unaffordable))]
        Unaffordable
    }
}
