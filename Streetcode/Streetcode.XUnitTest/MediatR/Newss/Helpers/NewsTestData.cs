namespace Streetcode.XUnitTest.MediatR.Newss.Helpers
{
    using Streetcode.BLL.DTO.Media.Images;
    using Streetcode.BLL.DTO.News;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.News;

    public static class NewsTestData
    {
        public static News CreateNews(int id = 1, string title = "Test News", int? imageId = 1)
        {
            return new News
            {
                Id = id,
                Title = title,
                Text = "Test text content",
                URL = $"test-url-{id}",
                ImageId = imageId,
                CreationDate = DateTime.Now,
            };
        }

        public static NewsDTO CreateNewsDTO(int id = 1, string title = "Test News", int? imageId = 1)
        {
            return new NewsDTO
            {
                Id = id,
                Title = title,
                Text = "Test text content",
                URL = $"test-url-{id}",
                ImageId = imageId,
                CreationDate = DateTime.Now,
                Image = imageId.HasValue ? CreateImageDTO(imageId.Value) : null,
            };
        }

        public static ImageDTO CreateImageDTO(int id = 1)
        {
            return new ImageDTO
            {
                Id = id,
                BlobName = $"test-blob-{id}",
                MimeType = "image/jpeg",
                Base64 = null,
            };
        }

        public static Image CreateImage(int id = 1)
        {
            return new Image
            {
                Id = id,
                BlobName = $"test-blob-{id}",
                MimeType = "image/jpeg",
            };
        }
    }
}
