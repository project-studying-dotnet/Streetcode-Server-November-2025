namespace Streetcode.XUnitTest.MediatR.Fixture
{
    public static class StreetcodeTestData
    {
        public static string CreatePersonStreetcode(
            int index = 1,
            string url = "test-john-doe",
            int? audioId = 7,
            int?[] tagIds = null,
            int?[] imgIds = null)
        {
            string tags = string.Empty;
            tagIds ??= new int?[] { 15, 20 };
            string imgs = string.Empty;
            imgIds ??= new int?[] { 10, 15 };

            if (tagIds.Length > 0)
            {
                tags = $@"
                {{""Id"": {(tagIds[0].HasValue ? tagIds[0].Value.ToString() : "null")}, ""IsVisible"": true}},
                {{""Id"": {(tagIds[1].HasValue ? tagIds[1].Value.ToString() : "null")}, ""IsVisible"": true}}";
            }

            if (imgIds.Length > 0)
            {
                imgs = $@"
                {{ ""ImageId"": {(imgIds[0].HasValue ? imgIds[0].Value.ToString() : "null")} }},
                {{ ""ImageId"": {(imgIds[1].HasValue ? imgIds[1].Value.ToString() : "null")} }}";
            }

            return $@"
            {{
              ""Index"": {index},
              ""Title"": ""Test Title"",
              ""StreetcodeType"": ""Person"",
              ""FirstName"": ""John"",
              ""LastName"": ""Doe"",
              ""TransliterationUrl"": ""{url}"",
              ""Date"": ""2024-12-03"",
              ""AudioId"": {(audioId.HasValue ? audioId.Value.ToString() : "null")},
              ""Tags"": [
                {tags}
              ],
              ""Images"": [
                {imgs}
              ]
            }}";
        }
    }
}
