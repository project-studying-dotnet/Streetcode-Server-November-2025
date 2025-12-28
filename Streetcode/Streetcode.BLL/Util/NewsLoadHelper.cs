using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.DTO.News;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.Helpers
{
    public static class NewsLoadHelper
    {
        public static async Task<Result<NewsDto>> LoadNewsAsync(
            string url,
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper,
            IBlobService blobService,
            ILoggerService logger)
        {
            var newsDto = mapper.Map<NewsDto>(await repositoryWrapper.NewsRepository.GetFirstOrDefaultAsync(
                predicate: sc => sc.URL == url,
                include: scl => scl
                    .Include(sc => sc.Image)));

            if (newsDto is null)
            {
                var errorMsg = string.Format(ErrorMessages.NewsNotFoundByUrl, url);
                logger.LogError(url, errorMsg);
                return Result.Fail(errorMsg);
            }

            if (newsDto.Image is not null)
            {
                newsDto.Image.Base64 = await blobService.FindFileInStorageAsBase64Async(newsDto.Image.BlobName);
            }

            return Result.Ok(newsDto);
        }
    }
}