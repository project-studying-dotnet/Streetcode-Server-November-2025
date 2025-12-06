using Microsoft.Extensions.Localization;

namespace Streetcode.BLL.Resources
{
    /// <summary>
    /// Helper class for accessing validation message resources.
    /// </summary>
    public class ValidationMessages
    {
        private readonly IStringLocalizer<ValidationMessages> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationMessages"/> class.
        /// </summary>
        /// <param name="localizer">The string localizer.</param>
        public ValidationMessages(IStringLocalizer<ValidationMessages> localizer)
        {
            _localizer = localizer;
        }

        /// <summary>
        /// Gets a localized validation message.
        /// </summary>
        /// <param name="key">The message key.</param>
        /// <param name="arguments">Optional format arguments.</param>
        /// <returns>The localized message.</returns>
        public string this[string key, params object[] arguments]
        {
            get
            {
                if (arguments == null || arguments.Length == 0)
                {
                    return _localizer[key];
                }

                return string.Format(_localizer[key], arguments);
            }
        }

        /// <summary>
        /// Gets a localized validation message without format arguments.
        /// </summary>
        /// <param name="key">The message key.</param>
        /// <returns>The localized message.</returns>
        public string Get(string key)
        {
            return _localizer[key];
        }

        /// <summary>
        /// Gets a localized validation message with format arguments.
        /// </summary>
        /// <param name="key">The message key.</param>
        /// <param name="arguments">Format arguments.</param>
        /// <returns>The localized message.</returns>
        public string Get(string key, params object[] arguments)
        {
            return string.Format(_localizer[key], arguments);
        }
    }
}
