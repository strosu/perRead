using PerRead.Backend.Models.Commands;

namespace PerRead.Backend.Services
{
    public class ImageService : IImageService
    {
        private IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> Save(string authorId, ArticleImage image)
        {
            var pathSuffix = $"uploads/{authorId}/{image.FileName}";

            var path = Path.Combine(_environment.WebRootPath, pathSuffix);
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            var sanitizedBase64 = image.Base64Encoded.Split(";base64,").Last();
            await File.WriteAllBytesAsync(path, Convert.FromBase64String(sanitizedBase64));

            return pathSuffix;
        }
    }

    public interface IImageService
    {
        /// <summary>
        /// Persists a base64 encoded image and returns its uri opath
        /// </summary>
        /// <param name="base64imageString"></param>
        /// <returns></returns>
        Task<string> Save(string authorId, ArticleImage image);
    }
}
