using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Streetcode.BLL.DTO.AdditionalContent.Tag;
using Streetcode.BLL.DTO.Media.Images;
using Streetcode.DAL.Enums;

namespace Streetcode.BLL.DTO.Streetcode
{
    public class UpdateStreetcodeDto
    {
        public int Id { get; set; }

        public int Index { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StreetcodeType StreetcodeType { get; set; }

        public string Title { get; set; } = null!;

        public DateTime EventStartOrPersonBirthDate { get; set; }

        public DateTime? EventEndOrPersonDeathDate { get; set; }

        public string? DateString { get; set; }

        public IEnumerable<StreetcodeTagDTO>? Tags { get; set; }

        [MaxLength(520)]
        public string? Teaser { get; set; }

        public List<ImageDetailsDto>? Images { get; set; }

        public int? AudioId { get; set; }

        [MaxLength(100)]
        public string TransliterationUrl { get; set; } = null!;

        public string? Description { get; set; }
    }
}
