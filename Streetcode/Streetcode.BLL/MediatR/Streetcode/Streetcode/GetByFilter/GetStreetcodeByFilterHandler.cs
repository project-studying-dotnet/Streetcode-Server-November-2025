using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.DTO.Streetcode;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.GetByFilter
{
    public class GetStreetcodeByFilterHandler : IRequestHandler<GetStreetcodeByFilterQuery, Result<List<StreetcodeFilterResultDto>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetStreetcodeByFilterHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<List<StreetcodeFilterResultDto>>> Handle(GetStreetcodeByFilterQuery request, CancellationToken cancellationToken)
        {
            string searchQuery = request.Filter.SearchQuery;
            var results = new List<StreetcodeFilterResultDto>();

            await AddStreetcodeResults(searchQuery, results);
            await AddTextResults(searchQuery, results);
            await AddFactResults(searchQuery, results);
            await AddTimelineResults(searchQuery, results);
            await AddArtResults(searchQuery, results);

            return results;
        }

        private async Task AddStreetcodeResults(string searchQuery, List<StreetcodeFilterResultDto> results)
        {
            var streetcodes = await _repositoryWrapper.StreetcodeRepository.GetAllAsync(
                predicate: x =>
                    x.Status == DAL.Enums.StreetcodeStatus.Published &&
                    (x.Title.Contains(searchQuery) ||
                    (x.Alias != null && x.Alias.Contains(searchQuery)) ||
                    x.Teaser.Contains(searchQuery)));

            foreach (var streetcode in streetcodes)
            {
                var matchingContent = GetMatchingStreetcodeContent(streetcode, searchQuery);
                if (matchingContent != null)
                {
                    results.Add(CreateFilterResult(streetcode, matchingContent));
                }
            }
        }

        private string? GetMatchingStreetcodeContent(StreetcodeContent streetcode, string searchQuery)
        {
            if (streetcode.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
            {
                return streetcode.Title;
            }

            if (!string.IsNullOrEmpty(streetcode.Alias) && streetcode.Alias.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
            {
                return streetcode.Alias;
            }

            if (streetcode.Teaser.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
            {
                return streetcode.Teaser;
            }

            if (streetcode.TransliterationUrl.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
            {
                return streetcode.TransliterationUrl;
            }

            return null;
        }

        private async Task AddTextResults(string searchQuery, List<StreetcodeFilterResultDto> results)
        {
            var texts = await _repositoryWrapper.TextRepository.GetAllAsync(
                include: i => i.Include(x => x.Streetcode),
                predicate: x => x.Streetcode.Status == DAL.Enums.StreetcodeStatus.Published);

            foreach (var text in texts)
            {
                var matchingContent = text.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                    ? text.Title
                    : (!string.IsNullOrEmpty(text.TextContent) && text.TextContent.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                        ? text.TextContent
                        : null);

                if (matchingContent != null)
                {
                    results.Add(CreateFilterResult(text.Streetcode, matchingContent, "Текст", "text"));
                }
            }
        }

        private async Task AddFactResults(string searchQuery, List<StreetcodeFilterResultDto> results)
        {
            var facts = await _repositoryWrapper.FactRepository.GetAllAsync(
                include: i => i.Include(x => x.Streetcode),
                predicate: x => x.Streetcode.Status == DAL.Enums.StreetcodeStatus.Published);

            foreach (var fact in facts)
            {
                if (fact.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    fact.FactContent.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(CreateFilterResult(fact.Streetcode, fact.Title, "Wow-факти", "wow-facts"));
                }
            }
        }

        private async Task AddTimelineResults(string searchQuery, List<StreetcodeFilterResultDto> results)
        {
            var timelineItems = await _repositoryWrapper.TimelineRepository.GetAllAsync(
                include: i => i.Include(x => x.Streetcode),
                predicate: x => x.Streetcode.Status == DAL.Enums.StreetcodeStatus.Published);

            foreach (var timelineItem in timelineItems)
            {
                if (timelineItem.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    (!string.IsNullOrEmpty(timelineItem.Description) && timelineItem.Description.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)))
                {
                    results.Add(CreateFilterResult(timelineItem.Streetcode, timelineItem.Title, "Хронологія", "timeline"));
                }
            }
        }

        private async Task AddArtResults(string searchQuery, List<StreetcodeFilterResultDto> results)
        {
            var streetcodeArts = await _repositoryWrapper.ArtRepository.GetAllAsync(
                include: i => i.Include(x => x.StreetcodeArts),
                predicate: x => x.StreetcodeArts.Any(art => art.Streetcode != null && art.Streetcode.Status == DAL.Enums.StreetcodeStatus.Published));

            foreach (var streetcodeArt in streetcodeArts)
            {
                if (!string.IsNullOrEmpty(streetcodeArt.Description) && streetcodeArt.Description.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var art in streetcodeArt.StreetcodeArts.Where(a => a.Streetcode != null))
                    {
                        results.Add(CreateFilterResult(art.Streetcode!, streetcodeArt.Description, "Арт-галерея", "art-gallery"));
                    }
                }
            }
        }

        private StreetcodeFilterResultDto CreateFilterResult(StreetcodeContent streetcode, string content, string? sourceName = null, string? blockName = null)
        {
            return new StreetcodeFilterResultDto
            {
                StreetcodeId = streetcode.Id,
                StreetcodeTransliterationUrl = streetcode.TransliterationUrl,
                StreetcodeIndex = streetcode.Index,
                BlockName = blockName,
                Content = content,
                SourceName = sourceName,
            };
        }
    }
}