using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.DTO.News;
using Streetcode.BLL.Helpers;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.News;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Newss.GetNewsAndLinksByUrl
{
    public class GetNewsAndLinksByUrlHandler : IRequestHandler<GetNewsAndLinksByUrlQuery, Result<NewsDtoWithUrls>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IBlobService _blobService;
        private readonly ILoggerService _logger;
        public GetNewsAndLinksByUrlHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, IBlobService blobService, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _blobService = blobService;
            _logger = logger;
        }

        public async Task<Result<NewsDtoWithUrls>> Handle(GetNewsAndLinksByUrlQuery request, CancellationToken cancellationToken)
        {
            string url = request.url;

            var newsResult = await NewsLoadHelper.LoadNewsAsync(
                url,
                _repositoryWrapper,
                _mapper,
                _blobService,
                _logger);

            if (newsResult.IsFailed)
            {
                return Result.Fail(newsResult.Errors);
            }

            var newsDto = newsResult.Value;

            var news = (await _repositoryWrapper.NewsRepository.GetAllAsync()).ToList();
            var newsIndex = news.FindIndex(x => x.Id == newsDto.Id);
            string prevNewsLink = null;
            string nextNewsLink = null;

            if(newsIndex != 0)
            {
                prevNewsLink = news[newsIndex - 1].URL;
            }

            if(newsIndex != news.Count - 1)
            {
                nextNewsLink = news[newsIndex + 1].URL;
            }

            var randomNewsTitleAndLink = new RandomNewsDto();

            var arrCount = news.Count;
            if (arrCount > 3)
            {
                if (newsIndex + 1 == arrCount - 1 || newsIndex == arrCount - 1)
                {
                    randomNewsTitleAndLink.RandomNewsUrl = news[newsIndex - 2].URL;
                    randomNewsTitleAndLink.Title = news[newsIndex - 2].Title;
                }
                else
                {
                    randomNewsTitleAndLink.RandomNewsUrl = news[arrCount - 1].URL;
                    randomNewsTitleAndLink.Title = news[arrCount - 1].Title;
                }
            }
            else
            {
                randomNewsTitleAndLink.RandomNewsUrl = news[newsIndex].URL;
                randomNewsTitleAndLink.Title = news[newsIndex].Title;
            }

            var newsDTOWithUrls = new NewsDtoWithUrls();
            newsDTOWithUrls.RandomNews = randomNewsTitleAndLink;
            newsDTOWithUrls.News = newsDto;
            newsDTOWithUrls.NextNewsUrl = nextNewsLink;
            newsDTOWithUrls.PrevNewsUrl = prevNewsLink;

            return Result.Ok(newsDTOWithUrls);
        }
    }
}