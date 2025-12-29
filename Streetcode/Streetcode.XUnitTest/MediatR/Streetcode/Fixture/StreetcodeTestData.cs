namespace Streetcode.XUnitTest.MediatR.Fixture
{
    public static class StreetcodeTestData
    {
        public static string CreatePersonStreetcode(
            int index = 1,
            string url = "test-john-doe",
            int? audioId = 7,
            int?[] tagIds = null,
            int?[] imgIds = null,
            int?[] commentsIds = null)
        {
            string tags = string.Empty;
            tagIds ??= new int?[] { 15, 20 };
            string imgs = string.Empty;
            imgIds ??= new int?[] { 10, 15 };
            string comments = string.Empty;
            commentsIds ??= new int?[] { 1, 2 };


            if (tagIds.Length > 0)
            {
                for (int i = 0; i < tagIds.Length; i++)
                {
                    tags += $@"
                    {{""Id"": {(tagIds[i].HasValue ? tagIds[i].Value.ToString() : "null")}, ""IsVisible"": true}},";
                }

                tags = tags.TrimEnd(',');
            }

            if (imgIds.Length > 0)
            {
                for (int i = 0; i < imgIds.Length; i++)
                {
                    imgs += $@"
                    {{ ""ImageId"": {(imgIds[i].HasValue ? imgIds[i].Value.ToString() : "null")} }},";
                }

                imgs = imgs.TrimEnd(',');
            }

            if (commentsIds.Length > 0)
            {
                for (int i = 0; i < commentsIds.Length; i++)
                {
                    comments += $@"
                    {{ ""Id"": {(commentsIds[i].HasValue ? commentsIds[i].Value.ToString() : "null")} }},";
                }

                comments = comments.TrimEnd(',');
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
              ],
              ""Comments"": [
                {comments}      
              ]
            }}";
        }

        public static string CreateNullValuesStreetcode()
        {
            return $@"
            {{
              ""Index"": 1,
              ""Title"": ""Test Title"",
              ""StreetcodeType"": ""Person"",
              ""FirstName"": ""John"",
              ""LastName"": ""Doe"",
              ""TransliterationUrl"": ""test-john-doe"",
              ""Date"": ""2024-12-03"",
              ""AudioId"": 7,
              ""Tags"": null,
              ""Images"": null,
              ""Comments"": null
            }}";
        }
    }
}
