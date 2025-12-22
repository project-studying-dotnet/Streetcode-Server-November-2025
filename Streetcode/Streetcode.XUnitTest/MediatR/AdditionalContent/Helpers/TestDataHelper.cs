namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers
{
    using Streetcode.BLL.DTO.AdditionalContent;
    using Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;
    using Streetcode.BLL.DTO.AdditionalContent.Subtitles;
    using Streetcode.BLL.DTO.AdditionalContent.Tag;
    using Streetcode.DAL.Entities.AdditionalContent;
    using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;

    public static class TestDataHelper
    {
        public static StreetcodeCoordinateDto CreateStreetcodeCoordinateDTO(int id = 1, decimal latitude = 50.4501m, decimal longtitude = 30.5234m)
        {
            return new StreetcodeCoordinateDto
            {
                Id = id,
                Latitude = latitude,
                Longtitude = longtitude,
            };
        }

        public static StreetcodeCoordinate CreateMappedCoordinate(int id = 1, decimal latitude = 50.4501m, decimal longtitude = 30.5234m)
        {
            return new StreetcodeCoordinate
            {
                Id = id,
                Latitude = latitude,
                Longtitude = longtitude,
            };
        }

        public static List<StreetcodeCoordinateDto> CreateStreetcodeCoordinateDtoList(int count = 2)
        {
            var dtos = new List<StreetcodeCoordinateDto>();
            for (int i = 1; i <= count; i++)
            {
                dtos.Add(CreateStreetcodeCoordinateDTO(
                    id: i,
                    latitude: 50.4501m + (i * 0.0001m),
                    longtitude: 30.5234m + (i * 0.0001m)));
            }

            return dtos;
        }

        public static List<StreetcodeCoordinate> CreateStreetcodeCoordinateList(int count = 2)
        {
            var coordinates = new List<StreetcodeCoordinate>();
            for (int i = 1; i <= count; i++)
            {
                coordinates.Add(CreateMappedCoordinate(
                    id: i,
                    latitude: 50.4501m + (i * 0.0001m),
                    longtitude: 30.5234m + (i * 0.0001m)));
            }

            return coordinates;
        }

        public static Subtitle CreateSubtitle(int id = 1, int streetCodeId = 1, string text = "testText")
        {
            return new Subtitle { Id = id, StreetcodeId = streetCodeId, Streetcode = null, SubtitleText = text };
        }

        public static SubtitleDto CreateSubtitleDto(int id = 1, int streetCodeId = 1, string text = "testText")
        {
            return new SubtitleDto { Id = id, StreetcodeId = streetCodeId, SubtitleText = text };
        }

        public static List<Subtitle> CreateSubtitles()
        {
            return new List<Subtitle>
            {
                CreateSubtitle(1),
                CreateSubtitle(2),
            };
        }

        public static List<SubtitleDto> CreateSubtitlesDtos()
        {
            return new List<SubtitleDto>
            {
                CreateSubtitleDto(1),
                CreateSubtitleDto(2),
            };
        }

        public static Tag CreateTag(int id = 1, string title = "testTitle")
        {
            return new Tag
            {
                Id = id,
                Title = title,
            };
        }

        public static TagDto CreateTagDto(int id = 1, string title = "testTitle")
        {
            return new TagDto
            {
                Id = id,
                Title = title,
            };
        }

        public static CreateTagDto CreateCreateTagDto(string title = "testTitle")
        {
            return new CreateTagDto { Title = title };
        }

        public static List<Tag> CreateTags()
        {
            return new List<Tag>
            {
                CreateTag(1),
                CreateTag(2),
            };
        }

        public static List<TagDto> CreateTagDtos()
        {
            return new List<TagDto>
            {
                CreateTagDto(1),
                CreateTagDto(2),
            };
        }

        public static StreetcodeTagIndex CreateStreetcodeTagIndex(
            int streetcodeId = 1,
            int tagId = 1,
            bool isVisible = true,
            int index = 1)
        {
            return new StreetcodeTagIndex
            {
                StreetcodeId = streetcodeId,
                TagId = tagId,
                IsVisible = isVisible,
                Index = index,
                Tag = CreateTag(tagId, string.Format("Tag {0}", tagId)),
            };
        }

        public static List<StreetcodeTagIndex> CreateStreetcodeTagIndexList()
        {
            return new List<StreetcodeTagIndex>
            {
                CreateStreetcodeTagIndex(streetcodeId: 1, tagId: 2, index: 2),
                CreateStreetcodeTagIndex(streetcodeId: 1, tagId: 1, index: 1),
                CreateStreetcodeTagIndex(streetcodeId: 1, tagId: 3, index: 3),
            };
        }

        public static StreetcodeTagDto CreateStreetcodeTagDto(
            int id = 1,
            string title = "Tag 1",
            bool isVisible = true,
            int index = 1)
        {
            return new StreetcodeTagDto
            {
                Id = id,
                Title = title,
                IsVisible = isVisible,
                Index = index,
            };
        }

        public static List<StreetcodeTagDto> CreateStreetcodeTagDtoList()
        {
            return new List<StreetcodeTagDto>
            {
                CreateStreetcodeTagDto(id: 1, title: "Tag 1", index: 1),
                CreateStreetcodeTagDto(id: 2, title: "Tag 2", index: 2),
                CreateStreetcodeTagDto(id: 3, title: "Tag 3", index: 3),
            };
        }
    }
}
