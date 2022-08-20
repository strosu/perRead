namespace PerRead.Backend.Models.FrontEnd
{
    public class FEArticleUnlockInfo
    {
        public string ArticleUnlockId { get; set; }

        public FEArticlePreview Article { get; set; }

        public uint AquisitionPrice { get; set; }

        public DateTime AquisitionDate { get; set; }
    }
}
