namespace Streetcode.BLL.Util.Validators
{
    /// <summary>
    /// Constants for field validation rules across the application.
    /// </summary>
    public static class ValidationConstants
    {
        /// <summary>
        /// Regular expression patterns for validation.
        /// </summary>
        public static class RegexPatterns
        {
            /// <summary>
            /// YouTube URL validation pattern. Matches youtube.com and youtu.be URLs.
            /// </summary>
            public const string YouTubeUrl =
                @"^(https?://)?(www\.)?(youtube\.com/(watch\?v=|embed/|v/)|youtu\.be/)[\w\-]+";

            /// <summary>
            /// Password pattern: at least one uppercase, one lowercase, one digit, min 6 chars (default Identity requirements, no special required by default).
            /// </summary>
            public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$";

            /// <summary>
            /// Username pattern: letters, digits, underscores, dot, dash only (per Identity default, no spaces or forbidden chars).
            /// </summary>
            public const string UserName = @"^[A-Za-z0-9_.-]+$";

            /// <summary>
            /// Phone number pattern: international format (starts with +, country code, digits only, 8-15 digits)
            /// </summary>
            public const string PhoneNumber = @"^\+[0-9]{8,15}$";
        }

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
            /// Minimum positive value threshold (for GreaterThan validation).
            /// </summary>
            public const int MinPositiveValue = 0;

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

            /// <summary>
            /// Maximum allowed size for images in bytes (5MB).
            /// </summary>
            public const long MaxImageSizeInBytes = 5 * 1024 * 1024;

            /// <summary>
            /// Maximum allowed size for audio files in bytes (10MB).
            /// </summary>
            public const long MaxAudioSizeInBytes = 10 * 1024 * 1024;
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

        /// <summary>
        /// Fact-specific validation constants.
        /// </summary>
        public static class Fact
        {
            /// <summary>
            /// Fact title maximum length.
            /// </summary>
            public const int TitleMaxLength = 100;

            /// <summary>
            /// Fact content maximum length.
            /// </summary>
            public const int ContentMaxLength = 600;
        }

        /// <summary>
        /// Text-specific validation constants.
        /// </summary>
        public static class Text
        {
            /// <summary>
            /// Text title maximum length.
            /// </summary>
            public const int TitleMaxLength = 500;

            /// <summary>
            /// Text content maximum length.
            /// </summary>
            public const int ContentMaxLength = 15000;

            /// <summary>
            /// Additional text maximum length.
            /// </summary>
            public const int AdditionalTextMaxLength = 1500;
        }

        /// <summary>
        /// RelatedTerm-specific validation constants.
        /// </summary>
        public static class RelatedTerm
        {
            /// <summary>
            /// Word maximum length.
            /// </summary>
            public const int WordMaxLength = 100;
        }

        /// <summary>
        /// Coordinate-specific validation constants.
        /// </summary>
        public static class Coordinate
        {
            /// <summary>
            /// Minimum valid latitude value.
            /// </summary>
            public const decimal MinLatitude = -90;

            /// <summary>
            /// Maximum valid latitude value.
            /// </summary>
            public const decimal MaxLatitude = 90;

            /// <summary>
            /// Minimum valid longitude value.
            /// </summary>
            public const decimal MinLongitude = -180;

            /// <summary>
            /// Maximum valid longitude value.
            /// </summary>
            public const decimal MaxLongitude = 180;
        }

        /// <summary>
        /// Payment-specific validation constants.
        /// </summary>
        public static class Payment
        {
            /// <summary>
            /// Maximum payment amount allowed.
            /// </summary>
            public const long MaxAmount = 1000000;

            /// <summary>
            /// Redirect URL maximum length.
            /// </summary>
            public const int RedirectUrlMaxLength = 500;
        }

        /// <summary>
        /// Tag-specific validation constants.
        /// </summary>
        public static class Tag
        {
            /// <summary>
            /// Tag title maximum length.
            /// </summary>
            public const int TitleMaxLength = 50;
        }

        /// <summary>
        /// User-specific validation constants.
        /// </summary>
        public static class User
        {
            public const int NameMaxLength = 50;
            public const int SurnameMaxLength = 50;
            public const int UserNameMaxLength = 20;
            public const int EmailMaxLength = 100;
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 20;
        }

        /// <summary>
        /// Term-specific validation constants.
        /// </summary>
        public static class Term
        {
            /// <summary>
            /// Term title maximum length.
            /// </summary>
            public const int TitleMaxLength = 50;

            /// <summary>
            /// Term description maximum length.
            /// </summary>
            public const int DescriptionMaxLength = 500;
        }

        /// <summary>
        /// Comment-specific validation constants.
        /// </summary>
        public static class Comment
        {
            /// <summary>
            /// Comment content maximum length.
            /// </summary>
            public const int ContentMaxLength = 1000;

            /// <summary>
            /// Comment author name maximum length.
            /// </summary>
            public const int AuthorNameMaxLength = 100;
        }
    }
}