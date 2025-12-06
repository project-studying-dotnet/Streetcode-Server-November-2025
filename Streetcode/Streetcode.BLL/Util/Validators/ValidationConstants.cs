namespace Streetcode.BLL.Util.Validators
{
    /// <summary>
    /// Constants for field validation rules across the application.
    /// </summary>
    public static class ValidationConstants
    {
        /// <summary>
        /// Common validation constants.
        /// </summary>
        public static class Common
        {
            /// <summary>
            /// Minimum valid ID value.
            /// </summary>
            public const int MinId = 1;

            /// <summary>
            /// Standard title maximum length.
            /// </summary>
            public const int TitleMaxLength = 255;

            /// <summary>
            /// Standard short title maximum length.
            /// </summary>
            public const int ShortTitleMaxLength = 100;

            /// <summary>
            /// MIME type maximum length.
            /// </summary>
            public const int MimeTypeMaxLength = 10;
        }

        /// <summary>
        /// Partner-specific validation constants.
        /// </summary>
        public static class Partner
        {
            /// <summary>
            /// Partner title maximum length.
            /// </summary>
            public const int TitleMaxLength = 255;

            /// <summary>
            /// Partner description maximum length.
            /// </summary>
            public const int DescriptionMaxLength = 500;

            /// <summary>
            /// Partner URL title maximum length.
            /// </summary>
            public const int UrlTitleMaxLength = 255;
        }

        /// <summary>
        /// News-specific validation constants.
        /// </summary>
        public static class News
        {
            /// <summary>
            /// News title maximum length.
            /// </summary>
            public const int TitleMaxLength = 150;

            /// <summary>
            /// News URL maximum length.
            /// </summary>
            public const int UrlMaxLength = 100;
        }

        /// <summary>
        /// Team-specific validation constants.
        /// </summary>
        public static class Team
        {
            /// <summary>
            /// Position name maximum length.
            /// </summary>
            public const int NameMaxLength = 50;
        }

        /// <summary>
        /// Media-specific validation constants.
        /// </summary>
        public static class Media
        {
            /// <summary>
            /// Image title maximum length.
            /// </summary>
            public const int TitleMaxLength = 100;

            /// <summary>
            /// Image alt text maximum length.
            /// </summary>
            public const int AltMaxLength = 200;

            /// <summary>
            /// Audio description maximum length.
            /// </summary>
            public const int DescriptionMaxLength = 500;
        }

        /// <summary>
        /// Streetcode-specific validation constants.
        /// </summary>
        public static class Streetcode
        {
            /// <summary>
            /// Title maximum length.
            /// </summary>
            public const int TitleMaxLength = 255;

            /// <summary>
            /// Alias maximum length.
            /// </summary>
            public const int AliasMaxLength = 50;

            /// <summary>
            /// Short description maximum length.
            /// </summary>
            public const int ShortDescriptionMaxLength = 33;

            /// <summary>
            /// Date string maximum length.
            /// </summary>
            public const int DateStringMaxLength = 50;

            /// <summary>
            /// Teaser maximum length.
            /// </summary>
            public const int TeaserMaxLength = 520;

            /// <summary>
            /// Transliteration URL maximum length.
            /// </summary>
            public const int TransliterationUrlMaxLength = 100;

            /// <summary>
            /// Valid Streetcode types.
            /// </summary>
            public static readonly string[] ValidTypes = { "Event", "Person" };
        }
    }
}
