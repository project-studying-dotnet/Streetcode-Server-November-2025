namespace Streetcode.XUnitTest.MediatR.Fixture
{
    public static class StreetcodeData
    {
        public static string CreatePersonStreetcode(
            int index = 1,
            string url = "test-john-doe",
            int audioId = 7,
            int[] imgIds = null,
            int[] tagIds = null)
        {
            imgIds ??= new[] { 15, 20 };
            tagIds ??= new[] { 10, 15 };

            return $@"
            {{
              ""Index"": {index},
              ""Title"": ""Test Title"",
              ""StreetcodeType"": ""Person"",
              ""FirstName"": ""John"",
              ""LastName"": ""Doe"",
              ""TransliterationUrl"": ""{url}"",
              ""Date"": ""2024-12-03"",
              ""AudioId"": {audioId} ,
              ""Tags"": [
                {{""Id"": {imgIds[0]}, ""IsVisible"": true}},
                {{""Id"": {imgIds[1]}, ""IsVisible"": true}}
              ],
              ""Images"": [
                {{ ""ImageId"": {tagIds[0]} }},
                {{ ""ImageId"": {tagIds[1]} }}
              ]
            }}";
        }
    }
}
