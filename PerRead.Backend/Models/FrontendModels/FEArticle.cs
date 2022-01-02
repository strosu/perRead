using System;
namespace PerRead.Backend.Models.FrontendModels
{
    public class FEArticleDescription
    {
        public string Title { get; set; }

        public IEnumerable<FEAuthorDescription> Authors { get; set; }

        public int Price { get; set; }

        public IEnumerable<FETagDescription> Tags { get; set; }
    }

    public class FEArticle
    {
        public string Title { get; set; }

        public IEnumerable<FEAuthorDescription> Authors { get; set; }

        public IEnumerable<FETagDescription> Tags { get; set; }

        public string Content { get; set; }
    }

    public class FETagDescription
    {
        public string Name { get; set; }

        public string TagUrl { get; set; }
    }

    public class FEAuthorDescription
    {
        public string Name { get; set; }

        public string ProfileUrl { get; set; }
    }
}

